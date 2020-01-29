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
    public abstract class TestStepXml : ITestStep
    {
        /// <inheritdoc/>
        public abstract string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether you should execute this step or skip it.
        /// </summary>
        public abstract bool ShouldExecute { get; set; }

        /// <inheritdoc/>
        public abstract int TestStepNumber { get; set; }

        /// <inheritdoc/>
        public abstract ITestStepStatus TestStepStatus { get; set; }

        /// <inheritdoc/>
        public abstract IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets the information for the test step.
        /// </summary>
        public abstract XmlNode TestStepInfo { get; set; }

        /// <summary>
        /// Gets or sets the seleniumDriver to use.
        /// </summary>
        public abstract SeleniumDriver Driver { get; set; }

        /// <inheritdoc/>
        public abstract void Execute();

        /// <inheritdoc/>
        public abstract void HandleException(Exception e);

        /// <inheritdoc/>
        public abstract void SetUp();

        /// <inheritdoc/>
        public abstract bool ShouldExecute();

        /// <inheritdoc/>
        public abstract void TearDown();
    }
}
