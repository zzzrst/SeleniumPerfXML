// <copyright file="TestSetXml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using AutomationTestSetFramework;
    using SeleniumPerfXML.Implementations.Loggers_and_Reporters;

    /// <summary>
    /// Implementation of the ITestSet Class.
    /// </summary>
    public class TestSetXml : ITestSet
    {
        /// <summary>
        /// Gets or sets a value indicating whether you should execute this step or skip it.
        /// </summary>
        public bool ShouldExecuteVariable { get; set; } = true;

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public int TotalTestCases
        {
            get => this.TestCaseFlow.ChildNodes.Count;
            set => this.TotalTestCases = this.TestCaseFlow.ChildNodes.Count;
        }

        /// <inheritdoc/>
        public ITestSetStatus TestSetStatus { get; set; }

        /// <inheritdoc/>
        public int CurrTestCaseNumber { get; set; } = 0;

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets the information for the test set.
        /// </summary>
        public XmlNode TestCaseFlow { get; set; }

        /// <summary>
        /// Gets or sets the reporter.
        /// </summary>
        public IReporter Reporter { get; set; }

        /// <summary>
        /// Gets or sets the test step logger.
        /// </summary>
        public ITestSetLogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the seleniumDriver to use.
        /// </summary>
        public SeleniumDriver Driver { get; set; }

        /// <summary>
        /// Gets or sets list of testcases to run.
        /// </summary>
        private TestCaseXml CurrTestCase { get; set; }

        /// <inheritdoc/>
        public bool ExistNextTestCase()
        {
            return this.CurrTestCaseNumber < this.TotalTestCases;
        }

        /// <inheritdoc/>
        public ITestCase GetNextTestCase()
        {
            TestCaseXml testCase = null;
            XmlNode currentNode = this.TestCaseFlow.ChildNodes[this.CurrTestCaseNumber];
            if (currentNode.Name == "If")
            {
                testCase = this.RunIfTestCase(currentNode);
            }
            else if (currentNode.Name == "RunTestCase")
            {
                testCase = this.FindTestCase(XMLInformation.ReplaceIfToken(currentNode.InnerText));
            }
            else
            {
                ///Logger.Warn($"We currently do not deal with this: {testStep.Name}");
            }
            //testCase = this.InnerFlow(currentNode, true);
            this.CurrTestCaseNumber += 1;
            return testCase;
        }

        /// <inheritdoc/>
        public void HandleException(Exception e)
        {
            this.CurrTestCaseNumber += 1;
            this.TestSetStatus.ErrorStack = e.StackTrace;
            this.TestSetStatus.FriendlyErrorMessage = e.Message;
            this.TestSetStatus.RunSuccessful = false;
        }

        /// <inheritdoc/>
        public void SetUp()
        {
            if (this.TestSetStatus == null)
            {
                this.TestSetStatus = new TestSetStatus()
                {
                    StartTime = DateTime.UtcNow,
                };
            }
        }

        /// <inheritdoc/>
        public bool ShouldExecute()
        {
            return this.ShouldExecuteVariable;
        }

        /// <inheritdoc/>
        public void TearDown()
        {
            this.TestSetStatus.EndTime = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public void UpdateTestSetStatus(ITestCaseStatus testCaseStatus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Runs the test case based on the provided ID.
        /// </summary>
        /// <param name="testCaseID">ID to find the testcase to run.</param>
        /// <param name="performAction"> Perfoms the action. </param>
        /// <returns> 0 if pass. >=1 if fail.</returns>
        private TestCaseXml FindTestCase(string testCaseID, bool performAction = true)
        {
            TestCaseXml testCase = null;

            // get the list of testcases
            XmlNode testCases = XMLInformation.XMLDocObj.GetElementsByTagName("TestCases")[0];

            // Find the appropriate testcase;
            foreach (XmlNode node in testCases.ChildNodes)
            {
                if (node.Name == "TestCase" && XMLInformation.ReplaceIfToken(node.Attributes["id"].Value) == testCaseID)
                {
                    int repeat = 1;
                    if (XMLInformation.RespectRepeatFor && node.Attributes["repeatFor"] != null)
                    {
                        repeat = int.Parse(node.Attributes["repeatFor"].Value);

                        // repeat = repeat > 1 ? 1 : -1;
                    }

                    testCase = new TestCaseXml()
                    {
                        TestCaseInfo = node,
                        ShouldExecuteAmountOfTimes = repeat,
                        ShouldExecuteVariable = performAction,
                        Reporter = this.Reporter,
                        Driver = this.Driver,
                    };
                    return testCase;
                }
            }

            //Logger.Warn($"Sorry, we didn't find a test case that matched the provided ID: {testCaseID}");
            return testCase;
        }

        /// <summary>
        /// This function parses the if test case flow and starts executing.
        /// </summary>
        /// <param name="ifXMLNode"> XML Node that has the if block. </param>
        /// <param name="performAction"> Perfoms the action. </param>
        /// <returns>0 if pass. >=1 if fail.</returns>
        private TestCaseXml RunIfTestCase(XmlNode ifXMLNode, bool performAction = true)
        {
            TestCaseXml testCase = null;
            bool ifCondition = false;

            // we check condition if we have to perfom this action.
            if (performAction)
            {
                string elementXPath = XMLInformation.ReplaceIfToken(ifXMLNode.Attributes["elementXPath"].Value);
                string condition = ifXMLNode.Attributes["condition"].Value;

                SeleniumDriver.ElementState state = condition == "EXIST" ? SeleniumDriver.ElementState.Visible : SeleniumDriver.ElementState.Invisible;

                ifCondition = this.Driver.CheckForElementState(elementXPath, state);
            }

            // inside the testCaseFlow, you can only have either RunTestCase element or an If element.
            foreach (XmlNode ifSection in ifXMLNode.ChildNodes)
            {
                if (ifSection.Name == "Then")
                {
                    // we run this test case only if performAction is true, and the condition for the element has passed.
                    testCase = this.InnerFlow(ifSection, performAction && ifCondition);
                }
                else if (ifSection.Name == "ElseIf")
                {
                    // we check the condition if performAction is true and the previous if condition was false.
                    // we can only run the test case if performAction is true, previous if condition was false, and the current if condition is true.
                    bool secondIfCondition = false;

                    if (performAction && !ifCondition)
                    {
                        string elementXPath = XMLInformation.ReplaceIfToken(ifXMLNode.Attributes["elementXPath"].Value);
                        string condition = ifXMLNode.Attributes["condition"].Value;

                        SeleniumDriver.ElementState state = condition == "EXIST" ? SeleniumDriver.ElementState.Visible : SeleniumDriver.ElementState.Invisible;

                        secondIfCondition = this.Driver.CheckForElementState(elementXPath, state);
                    }

                    testCase = this.InnerFlow(ifSection, performAction && !ifCondition && secondIfCondition);

                    // update ifCondition to reflect if elseIf was run
                    ifCondition = !ifCondition && secondIfCondition;
                }
                else if (ifSection.Name == "Else")
                {
                    // at this point, we only run this action if performAction is true and the previous ifCondition was false.
                    testCase = this.InnerFlow(ifSection, performAction && !ifCondition);
                }
                else if (ifSection.Name == "RunTestCase")
                {
                    testCase = this.FindTestCase(XMLInformation.ReplaceIfToken(ifSection.InnerText), performAction);
                }
                else
                {
                    //Logger.Warn($"We currently do not deal with this. {ifSection.Name}");
                }
            }

            return testCase;
        }

        /// <summary>
        /// Runs the test case based on the provided XMLNode.
        /// </summary>
        /// <param name="innerNode"> Optional XmlNode to represent testCases. </param>
        /// <param name="performAction"> Performs the action. </param>
        /// <returns>0 if pass. >=1 if not pass.</returns>
        private TestCaseXml InnerFlow(XmlNode innerNode, bool performAction = true)
        {
            TestCaseXml testCase = null;

            // Run Each Test Step Here
            foreach (XmlNode node in innerNode)
            {
                if (node.Name == "If")
                {
                    testCase = this.RunIfTestCase(node, performAction);
                }
                else if (node.Name == "RunTestCase")
                {
                    testCase = this.FindTestCase(XMLInformation.ReplaceIfToken(node.InnerText), performAction);
                }
                else
                {
                    ///Logger.Warn($"We currently do not deal with this: {testStep.Name}");
                }
            }

            return testCase;
        }
    }
}
