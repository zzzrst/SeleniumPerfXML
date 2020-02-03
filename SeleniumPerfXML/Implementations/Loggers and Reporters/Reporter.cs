// <copyright file="Reporter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations.Loggers_and_Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutomationTestSetFramework;

    /// <summary>
    /// The implementation of the reporter class.
    /// </summary>
    public class Reporter : IReporter
    {
        /// <summary>
        /// Gets or sets the location to save the report to.
        /// </summary>
        public string SaveFileLocation { get; set; }

        /// <inheritdoc/>
        public void AddTestCaseStatus(ITestCaseStatus testCaseStatus)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void AddTestSetStatus(ITestSetStatus testSetStatus)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void AddTestStepStatusToTestCase(ITestStepStatus testStepStatus, ITestCaseStatus testCaseStatus)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Report()
        {
            throw new NotImplementedException();
        }
    }
}
