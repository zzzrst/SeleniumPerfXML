// <copyright file="MaximizeBrowser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System;
    using System.Xml;

    /// <summary>
    /// This class executes the action of closing the current tab.
    /// </summary>
    public class MaximizeBrowser : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "MaximizeBrowser";

        /// <inheritdoc/>
        public override void Execute()
        {
            this.Driver.Maximize();
        }
    }
}
