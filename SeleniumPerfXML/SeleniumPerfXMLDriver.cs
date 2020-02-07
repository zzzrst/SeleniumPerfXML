// <copyright file="SeleniumPerfXMLDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
    using System.Xml;
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

                DateTime start = DateTime.UtcNow;

                AutomationTestSetDriver.RunTestSet(testStep);
                testStep.Reporter.Report();

                builder.RunAODA();

                DateTime end = DateTime.UtcNow;

                XMLInformation.CSVLogger.AddResults($"Total, {Math.Abs((start - end).TotalSeconds)}");
                XMLInformation.CSVLogger.WriteOutResults();

                string resultString = testStep.TestSetStatus.RunSuccessful ? "successfull" : "not successful";
                Logger.Info($"SeleniumPerfXML has finished. It was {resultString}");
            }

            Environment.Exit(resultCode);
            return resultCode;
        }

        private static void ValidateXMLdocument(string xmlFile)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("http://qa/SeleniumPerf", "SeleniumPerf.xsd");
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
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
    }
}
