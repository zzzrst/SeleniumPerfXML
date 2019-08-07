// <copyright file="OpenBrowser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of opening the browser to the specified site.
    /// </summary>
    public class OpenBrowser : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "OpenBrowser";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            // seleniumDriver.NavigateToURL();
            string url = testActionInformation.Attributes["repeatFor"] == null ? string.Empty : testActionInformation.Attributes["url"].Value;
            seleniumDriver.NavigateToURL(url);
        }
    }
}
