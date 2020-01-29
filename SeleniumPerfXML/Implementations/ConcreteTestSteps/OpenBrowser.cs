// <copyright file="OpenBrowser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Xml;
    using AutomationTestSetFramework;

    /// <summary>
    /// This class executes the action of opening the browser to the specified site.
    /// </summary>
    public class CloseBrowser : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "OpenBrowser";

        /// <inheritdoc/>
        public override bool ShouldExecuteVariable { get; set; }

        /// <inheritdoc/>
        public override int TestStepNumber { get; set; }

        /// <inheritdoc/>
        public override ITestStepStatus TestStepStatus { get; set; }

        /// <inheritdoc/>
        public override IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <inheritdoc/>
        public override XmlNode TestStepInfo { get; set; }

        /// <inheritdoc/>
        public override SeleniumDriver Driver { get; set; }

        /// <inheritdoc/>
        public override void Execute()
        {
            string url = this.TestStepInfo.Attributes["url"] == null ? string.Empty : this.TestStepInfo.Attributes["url"].Value;
            this.Driver.NavigateToURL(url);
        }

        /// <inheritdoc/>
        public override void HandleException(Exception e)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void SetUp()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool ShouldExecute()
        {
            return this.ShouldExecuteVariable;
        }

        /// <inheritdoc/>
        public override void TearDown()
        {
            throw new NotImplementedException();
        }
    }
}
