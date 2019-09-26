// <copyright file="SwitchToTab.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Xml;

    /// <summary>
    /// This class executes the action of switching to tab x.
    /// </summary>
    public class SwitchToTab : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "SwitchToTab";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override int Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            int tabIndex = Convert.ToInt32(testActionInformation.Attributes["tabIndex"].Value);
            seleniumDriver.SwitchToTab(tabIndex);

            return 0;
        }
    }
}
