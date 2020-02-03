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

        /// <summary>
        /// Gets or sets the list of test set statuses.
        /// </summary>
        public List<ITestSetStatus> TestSetStatuses { get; set; }

        /// <summary>
        /// Gets or sets the list of test case statuses.
        /// </summary>
        public List<ITestCaseStatus> TestCaseStatuses { get; set; }

        /// <summary>
        /// Gets or sets the dictonary with key as Test Cases status and value as a list of Test step statuses.
        /// </summary>
        public Dictionary<ITestCaseStatus, List<ITestStepStatus>> TestCaseToTestSteps { get; set; }

        /// <inheritdoc/>
        public void AddTestCaseStatus(ITestCaseStatus testCaseStatus)
        {
            this.TestCaseStatuses.Add(testCaseStatus);
        }

        /// <inheritdoc/>
        public void AddTestSetStatus(ITestSetStatus testSetStatus)
        {
            this.TestSetStatuses.Add(testSetStatus);
        }

        /// <inheritdoc/>
        public void AddTestStepStatusToTestCase(ITestStepStatus testStepStatus, ITestCaseStatus testCaseStatus)
        {
            if (!this.TestCaseToTestSteps.ContainsKey(testCaseStatus))
            {
                this.TestCaseToTestSteps.Add(testCaseStatus, new List<ITestStepStatus>());
            }

            this.TestCaseToTestSteps[testCaseStatus].Add(testStepStatus);
        }

        /// <inheritdoc/>
        public void Report()
        {
            string str;
            foreach (ITestSetStatus testSetStatus in this.TestSetStatuses)
            {
                str = testSetStatus.RunSuccessful.ToString();
                str = testSetStatus.ErrorStack;
                str = testSetStatus.FriendlyErrorMessage;
                str = testSetStatus.StartTime.ToString();
                str = testSetStatus.EndTime.ToString();
                str = testSetStatus.Description;
                str = testSetStatus.Expected;
                str = testSetStatus.Actual;
            }

            foreach (ITestCaseStatus testCaseStatus in this.TestCaseStatuses)
            {
                str = testCaseStatus.RunSuccessful.ToString();
                str = testCaseStatus.ErrorStack;
                str = testCaseStatus.FriendlyErrorMessage;
                str = testCaseStatus.StartTime.ToString();
                str = testCaseStatus.EndTime.ToString();
                str = testCaseStatus.Description;
                str = testCaseStatus.Expected;
                str = testCaseStatus.Actual;

                if (this.TestCaseToTestSteps.ContainsKey(testCaseStatus))
                {
                    foreach (ITestStepStatus testStepStatus in this.TestCaseToTestSteps[testCaseStatus])
                    {
                        str = testStepStatus.RunSuccessful.ToString();
                        str = testStepStatus.ErrorStack;
                        str = testStepStatus.FriendlyErrorMessage;
                        str = testStepStatus.StartTime.ToString();
                        str = testStepStatus.EndTime.ToString();
                        str = testStepStatus.Description;
                        str = testStepStatus.Expected;
                        str = testStepStatus.Actual;
                    }
                }
            }
        }
    }
}
