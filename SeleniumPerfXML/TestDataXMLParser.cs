﻿// <copyright file="TestDataXMLParser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Xml;
    using SeleniumPerfXML.TestActions;

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

        [field:NonSerialized]
        private SeleniumDriver SeleniumDriver { get; set; }

        private CSVLogger CSVLogger { get; set; }

        private int TestCaseRepeatNumber { get; set; } = -1;

        private int TestStepNumber { get; set; } = 0;

        /// <summary>
        /// This function parses the test case flow and starts executing.
        /// </summary>
        public void RunTestCaseFlow()
        {
            this.ParseParameters();
            this.ParseSpecialElements();
            this.InstantiateSeleniumDriver();

            XmlNode testCaseFlow = this.XMLDocObj.GetElementsByTagName("TestCaseFlow")[0];

            DateTime start = DateTime.UtcNow;

            // inside the testCaseFlow, you can only have either RunTestCase element or an If element.
            foreach (XmlNode innerFlow in testCaseFlow.ChildNodes)
            {
                if (innerFlow.Name == "RunTestCase")
                {
                    this.FindAndRunTestCase(innerFlow.InnerText);
                }
                else if (innerFlow.Name == "If")
                {
                    this.RunIfTestCase(innerFlow);
                }
                else
                {
                    Logger.Warn($"We currently do not deal with this. {innerFlow.Name}");
                }
            }

            if (this.RespectRunAODAFlag)
            {
                this.SeleniumDriver.GenerateAODAResults(this.LogSaveFileLocation);
            }

            DateTime end = DateTime.UtcNow;
            this.CSVLogger.AddResults($"Total, {Math.Abs((start - end).TotalSeconds)}");
            this.CSVLogger.WriteOutResults();
        }

        /// <summary>
        /// This function is responsible for parsing parameters in the XML File and updating variables if not overriden.
        /// </summary>
        private void ParseParameters()
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

            string xmlFileName = this.XMLFile.Substring(this.XMLFile.LastIndexOf("\\") + 1);
            xmlFileName = xmlFileName.Substring(0, xmlFileName.Length - 4);

            this.CSVLogger = new CSVLogger(this.CsvSaveFileLocation + "\\" + $"{xmlFileName}.csv");
            this.CSVLogger.AddResults($"Transaction, {DateTime.Now.ToString("G")}");
            this.CSVLogger.AddResults($"Environment URL, {this.URL}");

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

            Directory.CreateDirectory(this.CsvSaveFileLocation);
            Directory.CreateDirectory(this.LogSaveFileLocation);
            Directory.CreateDirectory(this.ScreenshotSaveLocation);
        }

        /// <summary>
        /// This function is responsible for parsing the special elements in the XML File and updating variables if not overriden.
        /// </summary>
        private void ParseSpecialElements()
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

        /// <summary>
        /// Instantiates the selenim driver to be used in this test run.
        /// </summary>
        private void InstantiateSeleniumDriver()
        {
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

            this.SeleniumDriver = new SeleniumDriver(browser, TimeSpan.FromSeconds(this.TimeOutThreshold), this.Environment, this.URL, this.ScreenshotSaveLocation)
            {
                ErrorContainer = this.ErrorContainer,
                LoadingSpinner = this.LoadingSpinner,
            };
        }

        /// <summary>
        /// Runs the test case based on the provided ID.
        /// </summary>
        /// <param name="testCaseID">ID to find the testcase to run.</param>
        /// <param name="performAction"> Perfoms the action. </param>
        private void FindAndRunTestCase(string testCaseID, bool performAction = true)
        {
            // get the list of testcases
            XmlNode testCases = this.XMLDocObj.GetElementsByTagName("TestCases")[0];

            // Find the appropriate testcase;
            foreach (XmlNode testcase in testCases.ChildNodes)
            {
                if (testcase.Name == "TestCase" && testcase.Attributes["id"].Value == testCaseID)
                {
                    int repeat = 1;
                    if (this.RespectRepeatFor && testcase.Attributes["repeatFor"] != null)
                    {
                        repeat = int.Parse(testcase.Attributes["repeatFor"].Value);
                        this.TestCaseRepeatNumber = repeat > 1 ? 1 : -1;
                    }

                    for (int i = 0; i < repeat; i++)
                    {
                        this.TestStepNumber = 0;
                        this.TestCaseRepeatNumber = this.TestCaseRepeatNumber > 0 ? i + 1 : -1;
                        this.RunTestCase(testcase, performAction);
                    }

                    return;
                }
            }

            Logger.Warn($"Sorry, we didn't find a test case that matched the provided ID: {testCaseID}");
        }

        /// <summary>
        /// Runs the test case based on the provided XMLNode.
        /// </summary>
        /// <param name="testCase"> Optional XmlNode to represent testCases. </param>
        /// <param name="performAction"> Performs the action. </param>
        private void RunTestCase(XmlNode testCase, bool performAction = true)
        {
            // Run Each Test Step Here
            foreach (XmlNode testStep in testCase)
            {
                // the testStepFlow can be either RunTestStep or If
                if (testStep.Name == "RunTestStep")
                {
                    this.FindAndRunTestStep(testStep.InnerText, performAction);
                }
                else if (testStep.Name == "If")
                {
                    this.RunIfTestCase(testStep, performAction);
                }
                else if (testStep.Name == "RunTestCase")
                {
                    this.FindAndRunTestCase(testStep.InnerText, performAction);
                }
                else
                {
                    Logger.Warn($"We currently do not deal with this: {testStep.Name}");
                }
            }
        }

        /// <summary>
        /// This function parses the if test case flow and starts executing.
        /// </summary>
        /// <param name="ifXMLNode"> XML Node that has the if block. </param>
        /// <param name="performAction"> Perfoms the action. </param>
        private void RunIfTestCase(XmlNode ifXMLNode, bool performAction = true)
        {
            bool ifCondition = false;

            // we check condition if we have to perfom this action.
            if (performAction)
            {
                string elementXPath = ifXMLNode.Attributes["elementXPath"].Value;
                string condition = ifXMLNode.Attributes["condition"].Value;

                SeleniumDriver.ElementState state = condition == "EXIST" ? SeleniumDriver.ElementState.Visible : SeleniumDriver.ElementState.Invisible;

                ifCondition = this.SeleniumDriver.CheckForElementState(elementXPath, state);
            }

            // inside the testCaseFlow, you can only have either RunTestCase element or an If element.
            foreach (XmlNode ifSection in ifXMLNode.ChildNodes)
            {
                if (ifSection.Name == "Then")
                {
                    // we run this test case only if performAction is true, and the condition for the element has passed.
                    this.RunTestCase(ifSection, performAction && ifCondition);
                }
                else if (ifSection.Name == "ElseIf")
                {
                    // we check the condition if performAction is true and the previous if condition was false.
                    // we can only run the test case if performAction is true, previous if condition was false, and the current if condition is true.
                    bool secondIfCondition = false;

                    if (performAction && !ifCondition)
                    {
                        string elementXPath = ifXMLNode.Attributes["elementXPath"].Value;
                        string condition = ifXMLNode.Attributes["condition"].Value;

                        SeleniumDriver.ElementState state = condition == "EXIST" ? SeleniumDriver.ElementState.Visible : SeleniumDriver.ElementState.Invisible;

                        secondIfCondition = this.SeleniumDriver.CheckForElementState(elementXPath, state);
                    }

                    this.RunTestCase(ifSection, performAction && !ifCondition && secondIfCondition);

                    // update ifCondition to reflect if elseIf was run
                    ifCondition = !ifCondition && secondIfCondition;
                }
                else if (ifSection.Name == "Else")
                {
                    // at this point, we only run this action if performAction is true and the previous ifCondition was false.
                    this.RunTestCase(ifSection, performAction && !ifCondition);
                }
                else if (ifSection.Name == "RunTestCase")
                {
                    this.FindAndRunTestCase(ifSection.InnerText, performAction);
                }
                else
                {
                    Logger.Warn($"We currently do not deal with this. {ifSection.Name}");
                }
            }
        }

        /// <summary>
        /// This function will go through the list of steps and run the appropriate test step if found.
        /// </summary>
        /// <param name="testStepID"> The ID of the test step to run. </param>
        /// <param name="performAction"> Perfoms the action. </param>
        private void FindAndRunTestStep(string testStepID, bool performAction = true)
        {
            // get the list of testSteps
            XmlNode testSteps = this.XMLDocObj.GetElementsByTagName("TestSteps")[0];

            // Find the appropriate test steps
            foreach (XmlNode testStep in testSteps.ChildNodes)
            {
                if (testStep.Name != "#comment" && testStep.Attributes["id"].Value == testStepID)
                {
                    this.RunTestStep(testStep, performAction);
                    return;
                }
            }

            Logger.Warn($"Sorry, we didn't find a test step that matched the provided ID: {testStepID}");
        }

        private void RunTestStep(XmlNode testStep, bool performAction = true)
        {
            string name = testStep.Attributes["name"].Value;

            // initial value is respectRunAODAFlag
            // if we respect the flag, and it is not found, then default value is false.
            bool runAODA = this.RespectRunAODAFlag;
            if (runAODA)
            {
                if (testStep.Attributes["runAODA"] != null)
                {
                    runAODA = bool.Parse(testStep.Attributes["runAODA"].Value);
                }
                else
                {
                    runAODA = false;
                }
            }

            // populate runAODAPageName. Deault is Not provided.
            string runAODAPageName = "Not provided.";
            if (runAODA)
            {
                if (testStep.Attributes["runAODAPageName"] != null)
                {
                    runAODAPageName = testStep.Attributes["runAODAPageName"].Value;
                }
            }

            // log is true by default.
            bool log = true;
            if (testStep.Attributes["log"] != null)
            {
                log = bool.Parse(testStep.Attributes["log"].Value);
            }

            Logger.Debug($"Test step '{name}': runAODA->{runAODA} runAODAPageName->{runAODAPageName} log->{log}");

            TestAction action = ReflectiveGetter.GetEnumerableOfType<TestAction>()
                .Find(x => x.Description.Equals(testStep.Name));

            if (action == null)
            {
                Logger.Error($"Was not able to find the provided test action '{testStep}'.");
            }
            else
            {
                this.TestStepNumber++;

                string namePrepender = this.TestCaseRepeatNumber > 0 ? $"{this.TestCaseRepeatNumber}.{this.TestStepNumber} " : $"";
                action.Execute(log, $"{namePrepender}{name} ", performAction, runAODA, runAODAPageName, testStep, this.SeleniumDriver, this.CSVLogger);
            }
        }
    }
}
