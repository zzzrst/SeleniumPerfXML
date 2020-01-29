// <copyright file="TestStepStatus.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutomationTestSetFramework;

    /// <summary>
    /// Implementation of ITestStepStatus class.
    /// </summary>
    public class TestStepStatus : ITestStepStatus
    {
        /// <inheritdoc/>
        public int TestStepNumber { get; set; }

        /// <inheritdoc/>
        public bool RunSuccessful { get; set; }

        /// <inheritdoc/>
        public string ErrorStack { get; set; }

        /// <inheritdoc/>
        public string FriendlyErrorMessage { get; set; }

        /// <inheritdoc/>
        public DateTime StartTime { get; set; }

        /// <inheritdoc/>
        public DateTime EndTime { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string Expected { get; set; }

        /// <inheritdoc/>
        public string Actual { get; set; }
    }
}
