// <copyright file="TestCaseXml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using AutomationTestSetFramework;
    using SeleniumPerfXML.TestActions;

    /// <summary>
    /// Implementation of the testCase class.
    /// </summary>
    public class TestCaseXml : ITestCase
    {
        /// <summary>
        /// Gets or sets a value indicating whether you should execute this step or skip it.
        /// </summary>
        public bool ShouldExecuteVariable { get; set; } = true;

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public int TestCaseNumber { get; set; }

        /// <inheritdoc/>
        public int TotalTestSteps
        {
            get => this.TestCaseInfo.ChildNodes.Count;
            set => this.TotalTestSteps = this.TestCaseInfo.ChildNodes.Count;
        }

        /// <inheritdoc/>
        public ITestCaseStatus TestCaseStatus { get; set; }

        /// <inheritdoc/>
        public int CurrTestStepNumber { get; set; }

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets the ammount of times this should be ran.
        /// </summary>
        public int ShouldExecuteAmountOfTimes { get; set; } = 1;

        /// <summary>
        /// Gets or sets the ammount of times the test case has ran.
        /// </summary>
        public int ExecuteCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the information for the test case.
        /// </summary>
        public XmlNode TestCaseInfo { get; set; }

        /// <summary>
        /// Gets or sets the reporter.
        /// </summary>
        public IReporter Reporter { get; set; }

        /// <summary>
        /// Gets or sets the test step logger.
        /// </summary>
        public ITestCaseLogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the seleniumDriver to use.
        /// </summary>
        public SeleniumDriver Driver { get; set; }

        /// <summary>
        /// Gets or sets the teststeps to run.
        /// </summary>
        private TestStepXml CurrTestStep { get; set; }

        /// <inheritdoc/>
        public bool ExistNextTestStep()
        {
            return this.CurrTestStepNumber + 1 < this.TotalTestSteps || this.ShouldExecuteAmountOfTimes > this.ExecuteCount;
        }

        /// <inheritdoc/>
        public ITestStep GetNextTestStep()
        {
            ITestStep testStep = null;
            this.CurrTestStepNumber += 1;
            XmlNode currNode = this.TestCaseInfo.ChildNodes[this.CurrTestStepNumber];

            // reached end of loop, check if should loop again.
            if (this.CurrTestStepNumber == this.TotalTestSteps && this.ShouldExecuteAmountOfTimes >= this.ExecuteCount)
            {
                this.CurrTestStepNumber = 0;
                this.ExecuteCount += 1;
            }

            testStep = this.FindTestStep(XMLInformation.ReplaceIfToken(currNode.InnerText), this.ShouldExecuteVariable);

            return testStep;
        }

        /// <inheritdoc/>
        public void HandleException(Exception e)
        {
            this.ShouldExecuteAmountOfTimes -= 1;
            this.TestCaseStatus.ErrorStack = e.StackTrace;
            this.TestCaseStatus.FriendlyErrorMessage = e.Message;
            this.TestCaseStatus.RunSuccessful = false;
        }

        /// <inheritdoc/>
        public void SetUp()
        {
            if (this.TestCaseStatus == null)
            {
                this.TestCaseStatus = new TestCaseStatus()
                {
                    StartTime = DateTime.UtcNow,
                    TestCaseNumber = this.TestCaseNumber,
                };
            }
        }

        /// <inheritdoc/>
        public bool ShouldExecute()
        {
            return this.ShouldExecuteAmountOfTimes >= this.ExecuteCount;
        }

        /// <inheritdoc/>
        public void TearDown()
        {
            this.TestCaseStatus.EndTime = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public void UpdateTestCaseStatus(ITestStepStatus testStepStatus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function will go through the list of steps and run the appropriate test step if found.
        /// </summary>
        /// <param name="testStepID"> The ID of the test step to run. </param>
        /// <param name="performAction"> Perfoms the action. </param>
        /// <returns>0 if pass. >=1 if fail.</returns>
        private ITestStep FindTestStep(string testStepID, bool performAction = true)
        {
            ITestStep testStep = null;

            // get the list of testSteps
            XmlNode testSteps = XMLInformation.XMLDocObj.GetElementsByTagName("TestSteps")[0];

            // Find the appropriate test steps
            foreach (XmlNode innerNode in testSteps.ChildNodes)
            {
                if (innerNode.Name != "#comment" && XMLInformation.ReplaceIfToken(innerNode.Attributes["id"].Value) == testStepID)
                {
                    testStep = this.BuildTestStep(innerNode, performAction);
                    return testStep;
                }
            }

            //Logger.Warn($"Sorry, we didn't find a test step that matched the provided ID: {testStepID}");
            return testStep;
        }

        private ITestStep BuildTestStep(XmlNode testStepNode, bool performAction = true)
        {
            TestStepXml testStep = null;
            string name = XMLInformation.ReplaceIfToken(testStepNode.Attributes["name"].Value);

            // initial value is respectRunAODAFlag
            // if we respect the flag, and it is not found, then default value is false.
            bool runAODA = XMLInformation.RespectRunAODAFlag;
            if (runAODA)
            {
                if (testStepNode.Attributes["runAODA"] != null)
                {
                    runAODA = bool.Parse(testStepNode.Attributes["runAODA"].Value);
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
                if (testStepNode.Attributes["runAODAPageName"] != null)
                {
                    runAODAPageName = XMLInformation.ReplaceIfToken(testStepNode.Attributes["runAODAPageName"].Value);
                }
            }

            // log is true by default.
            bool log = true;
            if (testStepNode.Attributes["log"] != null)
            {
                log = bool.Parse(testStepNode.Attributes["log"].Value);
            }

            //Logger.Debug($"Test step '{name}': runAODA->{runAODA} runAODAPageName->{runAODAPageName} log->{log}");

            testStep = ReflectiveGetter.GetEnumerableOfType<TestStepXml>()
                .Find(x => x.Name.Equals(testStepNode.Name));

            if (testStep == null)
            {
                //Logger.Error($"Was not able to find the provided test action '{testStepNode}'.");
            }
            else
            {
                string namePrepender = this.ExecuteCount > 0 ? $"{this.ExecuteCount }.{this.CurrTestStepNumber} " : $"";

                for (int index = 0; index < testStepNode.Attributes.Count; index++)
                {
                    testStepNode.Attributes[index].InnerText = XMLInformation.ReplaceIfToken(testStepNode.Attributes[index].InnerText);
                }

                testStep.TestStepInfo = testStepNode;
                testStep.ShouldExecuteVariable = performAction;
                testStep.RunAODA = runAODA;
                testStep.RunAODAPageName = runAODAPageName;
                testStep.Driver = this.Driver;
                testStep.Reporter = this.Reporter;
            }

            return testStep;
        }
    }
}
