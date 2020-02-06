// <copyright file="TestStepLogger.cs" company="PlaceholderCompany">
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
    /// The Implemntation of the TestSetLogger.
    /// </summary>
    public class TestStepLogger : ITestStepLogger
    {
        /// <summary>
        /// Gets or sets the location to save the log to.
        /// </summary>
        public string SaveFileLocation { get; set; } = XMLInformation.LogSaveFileLocation + "\\Log.txt";

        /// <inheritdoc/>
        public void Log(ITestStep testStep)
        {
            ITestStepStatus testStepStatus = testStep.TestStepStatus;
            List<string> str = new List<string>();
            str.Add(this.Tab(2) + "Name:" + testStep.Name);
            str.Add(this.Tab(2) + "TestStepNumber:" + testStep.TestStepNumber.ToString());
            str.Add(this.Tab(2) + "OnExceptionFlowBehavior:" + testStep.OnExceptionFlowBehavior.ToString());
            str.Add(this.Tab(2) + "ShouldExecute:" + testStep.ShouldExecute().ToString());
            str.Add(this.Tab(2) + "RunSuccessful:" + testStepStatus.RunSuccessful.ToString());
            str.Add(this.Tab(2) + "ErrorStack:" + testStepStatus.ErrorStack);
            str.Add(this.Tab(2) + "FriendlyErrorMessage:" + testStepStatus.FriendlyErrorMessage);
            str.Add(this.Tab(2) + "StartTime:" + testStepStatus.StartTime.ToString());
            str.Add(this.Tab(2) + "EndTime:" + testStepStatus.EndTime.ToString());
            str.Add(this.Tab(2) + "Description:" + testStepStatus.Description);
            str.Add(this.Tab(2) + "Expected:" + testStepStatus.Expected);
            str.Add(this.Tab(2) + "Actual:" + testStepStatus.Actual);
            str.Add(this.Tab(2) + "----------------------------");

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
