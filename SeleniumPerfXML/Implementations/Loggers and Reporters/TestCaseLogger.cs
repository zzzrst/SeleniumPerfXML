// <copyright file="TestCaseLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations.Loggers_and_Reporters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using AutomationTestSetFramework;

    /// <summary>
    /// The Implemntation of the TestCaseLogger.
    /// </summary>
    public class TestCaseLogger : ITestCaseLogger
    {
        /// <summary>
        /// Gets or sets the location to save the log to.
        /// </summary>
        public string SaveFileLocation { get; set; } = XMLInformation.LogSaveFileLocation + "\\Log.txt";

        /// <inheritdoc/>
        public void Log(ITestCase testCase)
        {
            ITestCaseStatus testCaseStatus = testCase.TestCaseStatus;
            List<string> str = new List<string>();
            str.Add(this.Tab(1) + "Name:" + testCase.Name);
            str.Add(this.Tab(1) + "TestCaseNumber:" + testCase.TestCaseNumber);
            str.Add(this.Tab(1) + "TotalTestSteps:" + testCase.TotalTestSteps);
            str.Add(this.Tab(1) + "OnExceptionFlowBehavior:" + testCase.OnExceptionFlowBehavior.ToString());
            str.Add(this.Tab(1) + "TestCaseNumber:" + testCase.TestCaseNumber.ToString());
            str.Add(this.Tab(1) + "TotalTestSteps:" + testCase.TotalTestSteps.ToString());
            str.Add(this.Tab(1) + "RunSuccessful:" + testCaseStatus.RunSuccessful.ToString());
            str.Add(this.Tab(1) + "ErrorStack:" + testCaseStatus.ErrorStack);
            str.Add(this.Tab(1) + "FriendlyErrorMessage:" + testCaseStatus.FriendlyErrorMessage);
            str.Add(this.Tab(1) + "StartTime:" + testCaseStatus.StartTime.ToString());
            str.Add(this.Tab(1) + "EndTime:" + testCaseStatus.EndTime.ToString());
            str.Add(this.Tab(1) + "Description:" + testCaseStatus.Description);
            str.Add(this.Tab(1) + "Expected:" + testCaseStatus.Expected);
            str.Add(this.Tab(1) + "Actual:" + testCaseStatus.Actual);

            using (StreamWriter file =
                new StreamWriter(@$"{this.SaveFileLocation}", true))
            {
                foreach (string line in str)
                {
                    file.WriteLine(line);
                }

                file.Close();
            }
        }

        private string Tab(int indents = 1)
        {
            return string.Concat(Enumerable.Repeat("    ", indents));
        }
    }
}
