// <copyright file="CloseTab.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Xml;

    /// <summary>
    /// This class executes the action of closing the current tab.
    /// </summary>
    public class MaximizeBrowser : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "MaximizeBrowser";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override int Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            seleniumDriver.Maximize();
            return 0;
        }
    }
}
