// <copyright file="NavigateToURL.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of opening the browser to the specified site.
    /// </summary>
    public class NavigateToURL : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "NavigateToURL";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override int Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            // seleniumDriver.NavigateToURL();
            string url = testActionInformation.Attributes["url"].Value;
            seleniumDriver.NavigateToURL(url, false);
            return 0;
        }
    }
}
