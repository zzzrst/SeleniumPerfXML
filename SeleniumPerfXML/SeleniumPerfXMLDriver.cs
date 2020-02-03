// <copyright file="SeleniumPerfXMLDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
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
                    ReportSaveFileLocation = reportSaveFileLocation,
                    XMLFile = xmlFile,
                };

                testStep = builder.BuildTestSet();
                AutomationTestSetDriver.RunTestSet(testStep);
                testStep.Reporter.Report();

                string resultString = testStep.TestSetStatus.RunSuccessful ? "successfull" : "not successful";
                Logger.Info($"SeleniumPerfXML has finished. It was {resultString}");
            }

            Environment.Exit(resultCode);
            return resultCode;
        }
    }
}
