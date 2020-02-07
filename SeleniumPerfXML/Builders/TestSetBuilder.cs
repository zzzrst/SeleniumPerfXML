// <copyright file="TestSetBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using SeleniumPerfXML.Builders;
    using SeleniumPerfXML.Implementations;
    using SeleniumPerfXML.Implementations.Loggers_and_Reporters;

    /// <summary>
    /// The TestSetBuilder Class to initilize all the nessesary variables.
    /// </summary>
    public class TestSetBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestSetBuilder"/> class.
        /// </summary>
        /// <param name="xmlFile"> The file path to the XML file.</param>
        public TestSetBuilder(string xmlFile)
        {
            this.XMLFile = xmlFile;

            if (File.Exists(this.XMLFile))
            {
                this.XMLDocObj = new XmlDocument();
                this.XMLDocObj.Load(xmlFile);
            }
            else
            {
                Logger.Error("XML File could not be found!");
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
        public int TimeOutThreshold { get; set; } = 120;

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
        /// Gets or sets the report save file location.
        /// </summary>
        public string ReportSaveFileLocation { get; set; } = string.Empty;

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

        [field: NonSerialized]
        private SeleniumDriver SeleniumDriver { get; set; }

        private CSVLogger CSVLogger { get; set; }

        private XmlDocument XMLDataFile { get; set; }

        /// <summary>
        /// Builds a new test set based on the given parameters from initilization.
        /// </summary>
        /// <returns>Tbe Test Set.</returns>
        public TestSetXml BuildTestSet()
        {
            TestSetXml testSet;
            this.ParseParameters();
            this.ParseSpecialElements();
            this.InstantiateSeleniumDriver();
            this.InitilizeXMLInfo();

            Reporter reporter = new Reporter()
            {
                SaveFileLocation = this.ReportSaveFileLocation + "\\Report.txt",
            };

            XmlNode testCaseFlow = this.XMLDocObj.GetElementsByTagName("TestCaseFlow")[0];

            testSet = new TestSetXml()
            {
                TestCaseFlow = testCaseFlow,
                Reporter = reporter,
                Driver = this.SeleniumDriver,
            };

            return testSet;
        }

        /// <summary>
        /// Runs AODA If needed
        /// </summary>
        public void RunAODA()
        {
            if (this.RespectRunAODAFlag)
            {
                string tempFolder = $"{this.LogSaveFileLocation}\\temp\\";

                // Delete temp folder if exist and recreate
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }

                Directory.CreateDirectory(tempFolder);

                // Generate AODA Results
                this.SeleniumDriver.GenerateAODAResults(tempFolder);

                // Zip all the contents up & Timestamp it
                string zipFileName = $"AODA_Results_{DateTime.Now:MM_dd_yyyy_hh_mm_ss_tt}.zip";
                ZipFile.CreateFromDirectory(tempFolder, $"{this.LogSaveFileLocation}\\{zipFileName}");

                // Remove all remaining contents.
                Directory.Delete(tempFolder, true);
            }

            this.SeleniumDriver.Quit();
        }

        private void InitilizeXMLInfo()
        {
            XMLInformation.XMLDataFile = this.XMLDataFile;
            XMLInformation.XMLDocObj = this.XMLDocObj;
            XMLInformation.RespectRepeatFor = this.RespectRepeatFor;
            XMLInformation.RespectRunAODAFlag = this.RespectRunAODAFlag;
            XMLInformation.LogSaveFileLocation = this.LogSaveFileLocation;
        }

        /// <summary>
        /// This function is responsible for parsing parameters in the XML File and updating variables if not overriden.
        /// </summary>
        private void ParseParameters()
        {
            // Must parse data file first since values above it can be a token.
            if (this.DataFile == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("DataFile").Count > 0)
                {
                    this.DataFile = this.XMLDocObj.GetElementsByTagName("DataFile")[0].InnerText;
                    if (File.Exists(this.DataFile))
                    {
                        this.XMLDataFile = new XmlDocument();
                        this.XMLDataFile.Load(this.DataFile);
                    }
                    else
                    {
                        Logger.Error("XML File could not be found!");
                    }
                }
            }

            // URL has precedence over the environment.
            // Passed in parameters overide what is in the XML.
            if (this.Environment == string.Empty && this.URL == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("URL").Count > 0)
                {
                    this.URL = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("URL")[0].InnerText);
                }
                else
                {
                    this.Environment = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("Environment")[0].InnerText);
                    this.URL = ConfigurationManager.AppSettings[this.Environment].ToString();
                }
            }
            else if (this.Environment != string.Empty && this.URL == string.Empty)
            {
                this.URL = ConfigurationManager.AppSettings[this.Environment].ToString();
            }

            if (this.Browser == string.Empty)
            {
                this.Browser = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("Browser")[0].InnerText);
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
                    this.RespectRunAODAFlag = bool.Parse(this.XMLDocObj.GetElementsByTagName("RespectRunAODAFlag")[0].InnerText);
                }
            }
            else
            {
                this.RespectRunAODAFlag = bool.Parse(this.PassedInRespectRunAODAFlag);
            }

            if (this.TimeOutThreshold == 0)
            {
                this.TimeOutThreshold = int.Parse(this.XMLDocObj.GetElementsByTagName("TimeOutThreshold")[0].InnerText);
            }

            if (this.WarningThreshold == 0)
            {
                this.WarningThreshold = int.Parse(this.XMLDocObj.GetElementsByTagName("WarningThreshold")[0].InnerText);
            }

            if (this.CsvSaveFileLocation == string.Empty)
            {
                this.CsvSaveFileLocation = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("CSVSaveLocation")[0].InnerText);
            }

            string xmlFileName = this.XMLFile.Substring(this.XMLFile.LastIndexOf("\\") + 1);
            xmlFileName = xmlFileName.Substring(0, xmlFileName.Length - 4);

            this.CSVLogger = new CSVLogger(this.CsvSaveFileLocation + "\\" + $"{xmlFileName}.csv");
            this.CSVLogger.AddResults($"Transaction, {DateTime.Now.ToString("G")}");
            this.CSVLogger.AddResults($"Environment URL, {this.URL}");

            if (this.LogSaveFileLocation == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("LogSaveLocation").Count > 0)
                {
                    this.LogSaveFileLocation = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("LogSaveLocation")[0].InnerText);
                }
                else
                {
                    this.LogSaveFileLocation = this.CsvSaveFileLocation;
                }
            }

            if (this.ReportSaveFileLocation == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("ReportSaveFileLocation").Count > 0)
                {
                    this.ReportSaveFileLocation = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("ReportSaveFileLocation")[0].InnerText);
                }
                else
                {
                    this.ReportSaveFileLocation = this.CsvSaveFileLocation;
                }
            }

            if (this.ScreenshotSaveLocation == string.Empty)
            {
                if (this.XMLDocObj.GetElementsByTagName("ScreenshotSaveLocation").Count > 0)
                {
                    this.ScreenshotSaveLocation = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("ScreenshotSaveLocation")[0].InnerText);
                }
                else
                {
                    this.ScreenshotSaveLocation = this.CsvSaveFileLocation;
                }
            }

            Directory.CreateDirectory(this.CsvSaveFileLocation);
            Directory.CreateDirectory(this.LogSaveFileLocation);
            Directory.CreateDirectory(this.ReportSaveFileLocation);
            Directory.CreateDirectory(this.ScreenshotSaveLocation);
        }

        /// <summary>
        /// This function is responsible for parsing the special elements in the XML File and updating variables if not overriden.
        /// </summary>
        private void ParseSpecialElements()
        {
            if (this.XMLDocObj.GetElementsByTagName("LoadingSpinner").Count > 0)
            {
                this.LoadingSpinner = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("LoadingSpinner")[0].InnerText);
            }
            else
            {
                this.LoadingSpinner = ConfigurationManager.AppSettings["LoadingSpinner"].ToString();
            }

            if (this.XMLDocObj.GetElementsByTagName("ErrorContainer").Count > 0)
            {
                this.ErrorContainer = XMLInformation.ReplaceIfToken(this.XMLDocObj.GetElementsByTagName("ErrorContainer")[0].InnerText);
            }
            else
            {
                this.ErrorContainer = ConfigurationManager.AppSettings["ErrorContainer"].ToString();
            }
        }

        /// <summary>
        /// Instantiates the selenim driver to be used in this test run.
        /// </summary>
        private void InstantiateSeleniumDriver()
        {
            SeleniumDriverBuilder builder;
            SeleniumDriver.Browser browser = SeleniumDriver.Browser.Chrome;

            if (this.Browser.ToLower().Contains("chrome"))
            {
                browser = SeleniumDriver.Browser.Chrome;
            }
            else if (this.Browser.ToLower().Contains("ie"))
            {
                browser = SeleniumDriver.Browser.IE;
            }
            else if (this.Browser.ToLower().Contains("firefox"))
            {
                browser = SeleniumDriver.Browser.Firefox;
            }
            else if (this.Browser.ToLower().Contains("edge"))
            {
                browser = SeleniumDriver.Browser.Edge;
            }

            builder = new SeleniumDriverBuilder()
            {
                Browser = browser,
                TimeOutThreshold = TimeSpan.FromSeconds(this.TimeOutThreshold),
                Environment = this.Environment,
                URL = this.URL,
                ScreenshotSaveLocation = this.ScreenshotSaveLocation,
                ErrorContainer = this.ErrorContainer,
                LoadingSpinner = this.LoadingSpinner,
            };
            this.SeleniumDriver = builder.BuildSeleniumDriver();
        }
    }
}
