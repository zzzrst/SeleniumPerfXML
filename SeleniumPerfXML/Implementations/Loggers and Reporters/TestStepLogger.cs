// <copyright file="TestStepLogger.cs" company="PlaceholderCompany">
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
    public class TestStepLogger : ITestStepLogger
    {
        /// <inheritdoc/>
        public void Log(ITestStep testStep)
        {
            ITestStepStatus testStepStatus = testStep.TestStepStatus;
            string str;
            str = testStep.Name;
            str = testStep.TestStepNumber.ToString();
            str = testStep.OnExceptionFlowBehavior.ToString();
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
