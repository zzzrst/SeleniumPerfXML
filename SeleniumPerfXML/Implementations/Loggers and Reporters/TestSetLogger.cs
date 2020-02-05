// <copyright file="TestSetLogger.cs" company="PlaceholderCompany">
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
    public class TestSetLogger : ITestSetLogger
    {
        /// <summary>
        /// Gets or sets the location to save the log to.
        /// </summary>
        public string SaveFileLocation { get; set; } = XMLInformation.LogSaveFileLocation + "\\Log.txt";

        /// <inheritdoc/>
        public void Log(ITestSet testSet)
        {
            ITestSetStatus testSetStatus = testSet.TestSetStatus;
            List<string> str = new List<string>();
            str.Add("Name:" + testSet.Name);
            str.Add("OnExceptionFlowBehavior:" + testSet.OnExceptionFlowBehavior.ToString());
            str.Add("TotalTestCases:" + testSet.TotalTestCases.ToString());
            str.Add("RunSuccessful:" + testSetStatus.RunSuccessful.ToString());
            str.Add("ErrorStack:" + testSetStatus.ErrorStack);
            str.Add("FriendlyErrorMessage:" + testSetStatus.FriendlyErrorMessage);
            str.Add("StartTime:" + testSetStatus.StartTime.ToString());
            str.Add("EndTime:" + testSetStatus.EndTime.ToString());
            str.Add("Description:" + testSetStatus.Description);
            str.Add("Expected:" + testSetStatus.Expected);
            str.Add("Actual:" + testSetStatus.Actual);

            using (StreamWriter file =
                new StreamWriter(@$"{this.SaveFileLocation}", true))
            {
                foreach (string line in str)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}
