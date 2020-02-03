// <copyright file="TestSetLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations.Loggers_and_Reporters
{
    using System;
    using System.Collections.Generic;
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
        public string SaveFileLocation { get; set; }

        /// <inheritdoc/>
        public void Log(ITestSet testSet)
        {
            ITestSetStatus testSetStatus = testSet.TestSetStatus;
            string str;
            str = testSet.Name;
            str = testSet.CurrTestCaseNumber.ToString();
            str = testSet.TotalTestCases.ToString();
            str = testSet.CurrTestCaseNumber.ToString();
            str = testSet.OnExceptionFlowBehavior.ToString();
            str = testSetStatus.RunSuccessful.ToString();
            str = testSetStatus.ErrorStack;
            str = testSetStatus.FriendlyErrorMessage;
            str = testSetStatus.StartTime.ToString();
            str = testSetStatus.EndTime.ToString();
            str = testSetStatus.Description;
            str = testSetStatus.Expected;
            str = testSetStatus.Actual;
        }
    }
}
