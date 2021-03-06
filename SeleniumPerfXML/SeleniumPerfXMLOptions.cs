﻿// <copyright file="SeleniumPerfXMLOptions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System.Collections.Generic;
    using CommandLine;
    using CommandLine.Text;

    /// <summary>
    /// This class stores the command line arguments that are taken in, both mandatory and optional.
    /// </summary>
    public class SeleniumPerfXMLOptions
    {
        /// <summary>
        /// Gets usage for SeleniumPerfXML.
        /// </summary>
        [Usage(ApplicationAlias = "SeleniumPerfXML.exe")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example(
                    "Passing in XML.",
                    new SeleniumPerfXMLOptions()
                    {
                        XMLFile = "C:\\SeleniumPerfXML\\SampleXML.xml",
                    });
                yield return new Example(
                    "Overriding paramters",
                    new SeleniumPerfXMLOptions()
                    {
                        Browser = "Chrome",
                        Environment = "SampleEnvironment",
                        RespectRepeatFor = "true",
                        RespectRunAodaFlag = "false",
                        TimeOutThreshold = 120,
                        WarningThreshold = 60,
                        CSVSaveFileLocation = "C:\\SeleniumPerfXML",
                        LogSaveLocation = "C:\\SeleniumPerfXML",
                        ReportSaveLocation = "C:\\SeleniumPerfXML",
                        ScreenShotSaveLocation = "C:\\SeleniumPerfXML\\ScreenShots",
                        XMLFile = "C:\\SeleniumPerfXML\\SampleXML.xml",
                    });
            }
        }

        /// <summary>
        /// Gets or sets the browser set in the XML.
        /// </summary>
        [Option('b', "browser", Required = false, HelpText = "Overrides the browser set in XML")]
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the environment set in the XML.
        /// </summary>
        [Option('e', "environment", Required = false, HelpText = "Overrides the environment set in XML")]
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the URL set in the XML. By default, the URL is derived by the environment provided.
        /// </summary>
        [Option('u', "url", Required = false, HelpText = "Overrides the url set in XML. Also overrides the derived URL from the environment.")]
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to respect RespectRepeatFor flag in the XML.
        /// </summary>
        [Option("respectRepeatFor", Required = false, HelpText = "Overrides the respect repeat for flag set in XML")]
        public string RespectRepeatFor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to respect RunAODAFlag in the XML.
        /// </summary>
        [Option("respectRunAODAFlag", Required = false, HelpText = "Overrides the respect run AODA flag set in XML")]
        public string RespectRunAodaFlag { get; set; }

        /// <summary>
        /// Gets or sets the timeout threshold in the XML.
        /// </summary>
        [Option("timeOutThreshold", Required = false, HelpText = "Overrides the timeout threshold set in XML")]
        public int TimeOutThreshold { get; set; }

        /// <summary>
        /// Gets or sets the warning threshold in the XML. Warning must be less than Timeout.
        /// </summary>
        [Option("warningThreshold", Required = false, HelpText = "Overrides the warning threshold set in XML")]
        public int WarningThreshold { get; set; }

        /// <summary>
        /// Gets or sets the datafile in the XML.
        /// </summary>
        [Option("dataFile", Required = false, HelpText = "Overrides the data file location set in XML")]
        public string DataFile { get; set; }

        /// <summary>
        /// Gets or sets the CSV file location in the XML.
        /// </summary>
        [Option("csvSaveFileLocation", Required = false, HelpText = "Overrides the csv save file location set in XML")]
        public string CSVSaveFileLocation { get; set; }

        /// <summary>
        /// Gets or sets the log save location in the XML.
        /// <para> </para>
        /// If not set, will use the directory path of CSVSaveFileLocation.
        /// </summary>
        [Option("logSaveLocation", Required = false, HelpText = "Overrides the log save location set in XML")]
        public string LogSaveLocation { get; set; }

        /// <summary>
        /// Gets or sets the log save location in the XML.
        /// <para> </para>
        /// If not set, will use the directory path of CSVSaveFileLocation.
        /// </summary>
        [Option("reportSaveLocation", Required = false, HelpText = "Overrides the report save location set in XML")]
        public string ReportSaveLocation { get; set; }

        /// <summary>
        /// Gets or sets the screenshot save location in the XML.
        /// <para> </para>
        /// If not set, will use the directory path of CSVSaveFileLocation.
        /// </summary>
        [Option("screenShotSaveLocation", Required = false, HelpText = "Overrides the screenshot save location set in XML")]
        public string ScreenShotSaveLocation { get; set; }

        /// <summary>
        /// Gets or sets the screenshot save location in the XML.
        /// <para> </para>
        /// If not set, will use the directory path of CSVSaveFileLocation.
        /// </summary>
        [Option("XMLFile", Required = true, HelpText = "XML file to be used.")]
        public string XMLFile { get; set; }
    }
}
