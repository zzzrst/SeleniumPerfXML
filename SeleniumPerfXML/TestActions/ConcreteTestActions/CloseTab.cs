// <copyright file="CloseTab.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// This class executes the action of closing the current tab.
    /// </summary>
    public class CloseTab : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "CloseTab";

        /// <inheritdoc/>
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver)
        {
            int tabIndex = Convert.ToInt32(testActionInformation.Attributes["tabIndex"].Value);
            seleniumDriver.SwitchToTab(tabIndex);
            seleniumDriver.CloseBrowser();
        }
    }
}
