// <copyright file="SeleniumPerfXMLDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using AutomationTestSetFramework;
    using CommandLine;
    using SeleniumPerfXML.Implementations;

    /// <summary>
    /// Driver class.
    /// </summary>
    public class SeleniumPerfXMLDriver
    {
        /// <summary>
        /// Main functionality.
        /// </summary>
        /// <param name="args"> The arguments to be passed in. </param>
        /// <returns> 0 if no errors were met. </returns>
        public static int Main(string[] args)
        {
            Logger.Info("Checking for updates...");
            if (CheckForUpdates(Assembly.GetExecutingAssembly().Location))
            {
                string newArgs = string.Join(" ", args.Select(x => string.Format("\"{0}\"", x)).ToList());
                Process p = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    FileName = "AutoUpdater.exe",
                    Arguments = newArgs,
                };

                p.StartInfo = startInfo;
                p.Start();

                Thread.Sleep(5000);

                // Closes the current process
                Environment.Exit(0);
            }
            else
            {
                Logger.Info("Program is up to date");
            }

            int resultCode = 0;
            bool errorParsing = false;

            // Paramaters to be used for XML.
            string browser = string.Empty;
            string environment = string.Empty;
            string url = string.Empty;
            string respectRunAODAFlag = string.Empty;
            string respectRepeatFor = string.Empty;
            int timeOutThreshold = 0;
            int warningThreshold = 0;
            string dataFile = string.Empty;
            string csvSaveFileLocation = string.Empty;
            string logSaveFileLocation = string.Empty;
            string reportSaveFileLocation = string.Empty;
            string screenshotSaveLocation = string.Empty;
            string xmlFile = string.Empty;

            Parser.Default.ParseArguments<SeleniumPerfXMLOptions>(args)
               .WithParsed<SeleniumPerfXMLOptions>(o =>
               {
                   browser = o.Browser ?? string.Empty;
                   environment = o.Environment ?? string.Empty;
                   url = o.URL ?? string.Empty;
                   respectRepeatFor = o.RespectRepeatFor ?? string.Empty;
                   respectRunAODAFlag = o.RespectRunAodaFlag ?? string.Empty;
                   timeOutThreshold = o.TimeOutThreshold;
                   warningThreshold = o.WarningThreshold;
                   dataFile = o.DataFile ?? string.Empty;
                   csvSaveFileLocation = o.CSVSaveFileLocation ?? string.Empty;
                   logSaveFileLocation = o.LogSaveLocation ?? string.Empty;
                   reportSaveFileLocation = o.ReportSaveLocation ?? string.Empty;
                   screenshotSaveLocation = o.ScreenShotSaveLocation ?? string.Empty;
                   xmlFile = o.XMLFile;
               })
               .WithNotParsed<SeleniumPerfXMLOptions>(errs =>
               {
                   Logger.Error(errs);
                   if (errs != null)
                   {
                       errorParsing = true;
                       resultCode = 1;
                   }
               });

            if (!errorParsing)
            {
                TestSetXml testStep;

                ValidateXMLdocument(xmlFile);

                TestSetBuilder builder = new TestSetBuilder(xmlFile)
                {
                    Browser = browser,
                    Environment = environment,
                    URL = url,
                    PassedInRespectRepeatFor = respectRepeatFor,
                    PassedInRespectRunAODAFlag = respectRunAODAFlag,
                    TimeOutThreshold = timeOutThreshold,
                    WarningThreshold = warningThreshold,
                    DataFile = dataFile,
                    CsvSaveFileLocation = csvSaveFileLocation,
                    LogSaveFileLocation = logSaveFileLocation,
                    ScreenshotSaveLocation = screenshotSaveLocation,
                    ReportSaveFileLocation = csvSaveFileLocation,
                    XMLFile = xmlFile,
                };
                testStep = builder.BuildTestSet();

                Logger.Info($"Running SeleniumPerfXML Version: {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}");

                DateTime start = DateTime.UtcNow;

                AutomationTestSetDriver.RunTestSet(testStep);
                testStep.Reporter.Report();

                builder.RunAODA();

                DateTime end = DateTime.UtcNow;

                XMLInformation.CSVLogger.AddResults($"Total, {Math.Abs((start - end).TotalSeconds)}");
                XMLInformation.CSVLogger.WriteOutResults();

                string resultString = testStep.TestSetStatus.RunSuccessful ? "successful" : "not successful";
                Logger.Info($"SeleniumPerfXML has finished. It was {resultString}");
            }

            return resultCode;
        }

        /// <summary>
        /// Checks to see if there is any update avalible.
        /// </summary>
        /// <param name="program">Name of the program to check.</param>.
        /// <returns>true if there are updates.</returns>
        private static bool CheckForUpdates(string program)
        {
            Version currentReleaseVersion = new Version(FileVersionInfo.GetVersionInfo(program).ProductVersion);

            // get the release version
            Version latestReleaseVersion = new Version(GetLatestReleaseVersion("https://github.com/zzzrst/SeleniumPerfXML/releases/latest"));

            Logger.Info($"Current Version: {currentReleaseVersion}");

            if (latestReleaseVersion.CompareTo(currentReleaseVersion) > 0)
            {
                Logger.Info($"Program is out of date! Version {latestReleaseVersion} is avaliable.");
                return true;
            }

            return false;
        }

        private static string GetLatestReleaseVersion(string url)
        {
            WebClient wc = new WebClient();
            string result = wc.DownloadString(url);
            Regex rx = new Regex("v[0-9]*[.][0-9]*[.][0-9]*");
            return rx.Match(result).Value.Substring(1);
        }

        private static void ValidateXMLdocument(string xmlFile)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("http://qa/SeleniumPerf", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SeleniumPerf.xsd");
            settings.ValidationType = ValidationType.Schema;

            XmlReader reader = XmlReader.Create(xmlFile, settings);
            XmlDocument document = new XmlDocument();
            document.Load(reader);

            ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

            // the following call to Validate succeeds.
            document.Validate(eventHandler);
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Logger.Error($"XML validation error: {e.Message} on Line: {e.Exception.LineNumber}");
                    break;
                case XmlSeverityType.Warning:
                    Logger.Warn($"XML validation warning: {e.Message} on Line: {e.Exception.LineNumber}");
                    break;
            }
        }
    }
}
