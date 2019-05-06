// <copyright file="TestDataXMLParser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// XMLParser that reads the XML passed in to create proper data structurs.
    /// </summary>
    public class TestDataXMLParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestDataXMLParser"/> class.
        /// </summary>
        /// <param name="xmlFile"> The file path to the XML file.</param>
        public TestDataXMLParser(string xmlFile)
        {
            this.XMLFile = xmlFile;

            if (File.Exists(this.XMLFile))
            {
                this.XMLDocObj = new XmlDocument();
                this.XMLDocObj.Load(xmlFile);
            }
            else
            {
                Console.WriteLine("XML File could not be found!");
            }
        }

        /// <summary>
        /// Gets or sets the browser to use in this test.
        /// </summary>
        public string Browser { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the environment to go to.
        /// </summary>
        public string Environment { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL the browser should land on first.
        /// </summary>
        public string URL { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the passed in respectRunAODAFlag parameter.
        /// </summary>
        public string PassedInRespectRunAODAFlag { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the passed in respectRepeatFor parameter.
        /// </summary>
        public string PassedInRespectRepeatFor { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timeout threshold.
        /// </summary>
        public int TimeOutThreshold { get; set; } = 0;

        /// <summary>
        /// Gets or sets the warning threshold. Note that the warning threshold should be less than the timeout threshold.
        /// </summary>
        public int WarningThreshold { get; set; } = 0;

        /// <summary>
        /// Gets or sets the file path to the data file.
        /// </summary>
        public string DataFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the csv save file location.
        /// </summary>
        public string CsvSaveFileLocation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the log save file location.
        /// </summary>
        public string LogSaveFileLocation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the screenshot save location.
        /// </summary>
        public string ScreenshotSaveLocation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the XML file.
        /// </summary>
        public string XMLFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether to respectRunAODAFlag or not.
        /// </summary>
        private bool RespectRunAODAFlag { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to respectRepeatFor or not.
        /// </summary>
        private bool RespectRepeatFor { get; set; } = false;

        private XmlDocument XMLDocObj { get; set; }

        private string LoadingSpinner { get; set; }

        private string ErrorContainer { get; set; }

        /// <summary>
        /// This function is responsible for parsing parameters in the XML File and updating variables if not overriden.
        /// </summary>
        public void ParseParameters()
        {
            // URL has precedence over the environment.
            // Passed in parameters overide what is in the XML.
            if (this.Environment == string.Empty && this.URL == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("URL").Count > 0)
                {
                    this.URL = this.XMLDocObj.GetElementsByTagName("URL")[0].InnerText;
                }
                else
                {
                    this.Environment = this.XMLDocObj.GetElementsByTagName("Environment")[0].InnerText;
                    this.URL = ConfigurationManager.AppSettings[this.Environment].ToString();
                }
            }

            if (this.Browser == string.Empty)
            {
                this.Browser = this.XMLDocObj.GetElementsByTagName("Browser")[0].InnerText;
            }

            if (this.PassedInRespectRepeatFor == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("RespectRepeatFor").Count > 0)
                {
                    this.RespectRepeatFor = bool.Parse(this.XMLDocObj.GetElementsByTagName("RespectRepeatFor")[0].InnerText);
                }
            }
            else
            {
                this.RespectRepeatFor = bool.Parse(this.PassedInRespectRepeatFor);
            }

            if (this.PassedInRespectRunAODAFlag == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("RespectRunAODAFlag").Count > 0)
                {
                    this.RespectRepeatFor = bool.Parse(this.XMLDocObj.GetElementsByTagName("RespectRunAODAFlag")[0].InnerText);
                }
            }
            else
            {
                this.RespectRepeatFor = bool.Parse(this.PassedInRespectRunAODAFlag);
            }

            if (this.TimeOutThreshold == 0)
            {
                this.TimeOutThreshold = int.Parse(this.XMLDocObj.GetElementsByTagName("TimeOutThreshold")[0].InnerText);
            }

            if (this.WarningThreshold == 0)
            {
                this.WarningThreshold = int.Parse(this.XMLDocObj.GetElementsByTagName("WarningThreshold")[0].InnerText);
            }

            if (this.DataFile == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("DataFile").Count > 0)
                {
                    this.DataFile = this.XMLDocObj.GetElementsByTagName("DataFile")[0].InnerText;
                }
            }

            if (this.CsvSaveFileLocation == string.Empty)
            {
                this.CsvSaveFileLocation = this.XMLDocObj.GetElementsByTagName("CSVSaveLocation")[0].InnerText;
            }

            if (this.LogSaveFileLocation == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("LogSaveLocation").Count > 0)
                {
                    this.LogSaveFileLocation = this.XMLDocObj.GetElementsByTagName("LogSaveLocation")[0].InnerText;
                }
                else
                {
                    this.LogSaveFileLocation = this.CsvSaveFileLocation;
                }
            }

            if (this.ScreenshotSaveLocation == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("ScreenshotSaveLocation").Count > 0)
                {
                    this.ScreenshotSaveLocation = this.XMLDocObj.GetElementsByTagName("ScreenshotSaveLocation")[0].InnerText;
                }
                else
                {
                    this.ScreenshotSaveLocation = this.CsvSaveFileLocation;
                }
            }
        }

        /// <summary>
        /// This function is responsible for parsing the special elements in the XML File and updating variables if not overriden.
        /// </summary>
        public void ParseSpecialElements()
        {
            if (this.XMLDocObj.GetElementsByTagName("LoadingSpinner").Count > 0)
            {
                this.LoadingSpinner = this.XMLDocObj.GetElementsByTagName("LoadingSpinner")[0].InnerText;
            }
            else
            {
                this.LoadingSpinner = ConfigurationManager.AppSettings["LoadingSpinner"].ToString();
            }

            if (this.XMLDocObj.GetElementsByTagName("ErrorContainer").Count > 0)
            {
                this.ErrorContainer = this.XMLDocObj.GetElementsByTagName("ErrorContainer")[0].InnerText;
            }
            else
            {
                this.ErrorContainer = ConfigurationManager.AppSettings["ErrorContainer"].ToString();
            }
        }

        public void ParseTestCaseFlow()
        {

        }

        public void ParseTestCases()
        {

        }

        public void ParseTestSteps()
        {

        }
    }
}
