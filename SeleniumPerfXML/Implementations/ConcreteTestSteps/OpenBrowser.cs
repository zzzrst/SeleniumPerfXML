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
    public class OpenBrowser : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "OpenBrowser";

        /// <inheritdoc/>
        public override void Execute()
        {
            string url = this.TestStepInfo.Attributes["url"] == null ? string.Empty : this.TestStepInfo.Attributes["url"].Value;
            this.Driver.NavigateToURL(url);
        }

        /// <inheritdoc/>
        public override void HandleException(Exception e)
        {
        }

        /// <inheritdoc/>
        public override void SetUp()
        {
        }

        /// <inheritdoc/>
        public override bool ShouldExecute()
        {
            return base.ShouldExecute();
        }

        /// <inheritdoc/>
        public override void TearDown()
        {
        }
    }
}
