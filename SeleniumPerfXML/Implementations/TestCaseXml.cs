// <copyright file="TestCaseXml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
        public ITestCaseStatus TestCaseStatus { get; }

        /// <inheritdoc/>
        public int CurrTestStepNumber { get; set; }

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets the teststeps to run.
        /// </summary>
        public List<TestStepXml> TestSteps { get; set; }

        /// <inheritdoc/>
        public bool ExistNextTestStep()
        {
            return this.CurrTestStepNumber + 1 < this.TotalTestSteps;
        }

        /// <inheritdoc/>
        public ITestStep GetNextTestStep()
        {
            this.CurrTestStepNumber += 1;
            return this.TestSteps[this.CurrTestStepNumber];
        }

        /// <inheritdoc/>
        public void HandleException(Exception e)
        {
            this.TestCaseStatus.ErrorStack = e.StackTrace;
            this.TestCaseStatus.FriendlyErrorMessage = e.Message;
            this.TestCaseStatus.RunSuccessful = false;
        }

        /// <inheritdoc/>
        public void SetUp()
        {
            this.TestCaseStatus.StartTime = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public bool ShouldExecute()
        {
            return this.ShouldExecuteVariable;
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
