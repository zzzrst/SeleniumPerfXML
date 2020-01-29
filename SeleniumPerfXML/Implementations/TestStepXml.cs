// <copyright file="TestStepXml.cs" company="PlaceholderCompany">
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
        /// Gets or sets the ammount of times this should be ran.
        /// </summary>
        public int ShouldExecuteAmountOfTimes { get; set; } = 1;

        /// <inheritdoc/>
        public virtual void Execute()
        {
            // Override.
        }

        /// <inheritdoc/>
        public virtual void HandleException(Exception e)
        {
            this.TestStepStatus.ErrorStack = e.StackTrace;
            this.TestStepStatus.FriendlyErrorMessage = e.Message;
            this.TestStepStatus.RunSuccessful = false;
        }

        /// <inheritdoc/>
        public virtual void SetUp()
        {
            if (this.TestStepStatus == null)
            {
            }

            this.TestStepStatus.StartTime = DateTime.UtcNow;
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
        }
    }
}
