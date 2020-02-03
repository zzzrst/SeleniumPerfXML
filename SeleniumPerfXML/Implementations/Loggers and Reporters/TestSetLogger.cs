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
            throw new NotImplementedException();
        }
    }
}
