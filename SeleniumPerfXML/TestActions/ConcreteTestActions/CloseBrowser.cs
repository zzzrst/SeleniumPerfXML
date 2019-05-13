// <copyright file="CloseBrowser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// This class executes the action of closing the browser.
    /// </summary>
    public class CloseBrowser : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "CloseBrowser";

        /// <inheritdoc/>
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver)
        {
            seleniumDriver.CloseBrowser();
        }
    }
}
