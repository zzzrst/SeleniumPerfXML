// <copyright file="OpenBrowser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Xml;
    using AutomationTestSetFramework;

    /// <summary>
    /// This class executes the action of closing the browser.
    /// </summary>
    public class CloseBrowser : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "CloseBrowser";

        /// <inheritdoc/>
        public override void Execute()
        {
            this.Driver.CloseBrowser();
        }

        /// <inheritdoc/>
        public override void HandleException(Exception e)
        {
            throw new NotImplementedException();
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
