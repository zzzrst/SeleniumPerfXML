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
            get => this.TestSteps.Count;
            set => this.TotalTestSteps = this.TestSteps.Count;
        }

        /// <inheritdoc/>
        public ITestCaseStatus TestCaseStatus { get; set; }

        /// <inheritdoc/>
        public int CurrTestStepNumber { get; set; }

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets the teststeps to run.
        /// </summary>
        public List<TestStepXml> TestSteps { get; set; }

        /// <summary>
        /// Gets or sets the ammount of times this should be ran.
        /// </summary>
        public int ShouldExecuteAmountOfTimes { get; set; } = 1;

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

        /// <inheritdoc/>
        public bool ExistNextTestStep()
        {
            return this.CurrTestStepNumber + 1 < this.TotalTestSteps;
        }

        /// <inheritdoc/>
        public ITestStep GetNextTestStep()
        {
            ITestStep teststep = this.TestSteps[this.CurrTestStepNumber];
            this.CurrTestStepNumber += 1;

            if (this.CurrTestStepNumber == this.TestSteps.Count && this.ShouldExecuteAmountOfTimes > 1)
            {
                this.CurrTestStepNumber = 0;
                this.ShouldExecuteAmountOfTimes -= 1;
            }

            return teststep;
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
            return this.ShouldExecuteVariable && this.ShouldExecuteAmountOfTimes > 0;
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
    }
}
