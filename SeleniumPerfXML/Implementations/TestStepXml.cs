﻿// <copyright file="TestStepXml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using AutomationTestSetFramework;

    /// <summary>
    /// An Implementation of the ITestStep class.
    /// </summary>
    public class TestStepXml : ITestStep
    {
        /// <inheritdoc/>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether you should execute this step or skip it.
        /// </summary>
        public bool ShouldExecuteVariable { get; set; } = true;

        /// <inheritdoc/>
        public int TestStepNumber { get; set; }

        /// <inheritdoc/>
        public ITestStepStatus TestStepStatus { get; set; }

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets the information for the test step.
        /// </summary>
        public XmlNode TestStepInfo { get; set; }

        /// <summary>
        /// Gets or sets the seleniumDriver to use.
        /// </summary>
        public SeleniumDriver Driver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to run AODA.
        /// </summary>
        public bool RunAODA { get; set; } = false;

        /// <summary>
        /// Gets or sets the AODA page name.
        /// </summary>
        public string RunAODAPageName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reporter.
        /// </summary>
        public IReporter Reporter { get; set; }

        /// <summary>
        /// Gets or sets the test step logger.
        /// </summary>
        public ITestStepLogger Logger { get; set; }

        /// <inheritdoc/>
        public virtual void Execute()
        {
        }

        /// <inheritdoc/>
        public virtual void HandleException(Exception e)
        {
            this.TestStepStatus.ErrorStack = e.StackTrace;
            this.TestStepStatus.FriendlyErrorMessage = e.Message;
            this.TestStepStatus.RunSuccessful = false;

            this.Driver.TakeScreenShot();
        }

        /// <inheritdoc/>
        public virtual void SetUp()
        {
            if (this.TestStepStatus == null)
            {
                this.TestStepStatus = new TestStepStatus()
                {
                    StartTime = DateTime.UtcNow,
                    TestStepNumber = this.TestStepNumber,
                };
            }
        }

        /// <inheritdoc/>
        public virtual bool ShouldExecute()
        {
            return this.ShouldExecuteVariable;
        }

        /// <inheritdoc/>
        public virtual void TearDown()
        {
            this.TestStepStatus.EndTime = DateTime.UtcNow;
            if (this.RunAODA)
            {
                this.Driver.RunAODA(this.RunAODAPageName);
            }
        }
    }
}
