// <copyright file="TestCaseLogger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations.Loggers_and_Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutomationTestSetFramework;

    /// <summary>
    /// The Implemntation of the TestCaseLogger.
    /// </summary>
    public class TestCaseLogger : ITestCaseLogger
    {
        /// <inheritdoc/>
        public void Log(ITestCase testCase)
        {
            ITestCaseStatus testCaseStatus = testCase.TestCaseStatus;
            string str;
            str = testCase.CurrTestStepNumber.ToString();
            str = testCase.Name;
            str = testCase.OnExceptionFlowBehavior.ToString();
            str = testCase.TestCaseNumber.ToString();
            str = testCase.TotalTestSteps.ToString();
            str = testCaseStatus.RunSuccessful.ToString();
            str = testCaseStatus.ErrorStack;
            str = testCaseStatus.FriendlyErrorMessage;
            str = testCaseStatus.StartTime.ToString();
            str = testCaseStatus.EndTime.ToString();
            str = testCaseStatus.Description;
            str = testCaseStatus.Expected;
            str = testCaseStatus.Actual;
        }
    }
}
