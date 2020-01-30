// <copyright file="TestSetXml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutomationTestSetFramework;

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
        public string Name { get; set; }

        /// <inheritdoc/>
        public int TotalTestCases
        {
            get => this.TestCases.Count;
            set => this.TotalTestCases = this.TestCases.Count;
        }

        /// <inheritdoc/>
        public ITestSetStatus TestSetStatus { get; set; }

        /// <inheritdoc/>
        public int CurrTestCaseNumber { get; set; }

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets list of testcases to run.
        /// </summary>
        public List<TestCaseXml> TestCases { get; set; }

        /// <inheritdoc/>
        public bool ExistNextTestCase()
        {
            return this.CurrTestCaseNumber + 1 < this.TotalTestCases;
        }

        /// <inheritdoc/>
        public ITestCase GetNextTestCase()
        {
            this.CurrTestCaseNumber += 1;
            return this.TestCases[this.CurrTestCaseNumber];
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
    }
}
