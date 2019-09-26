// <copyright file="CloseBrowser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of closing the browser.
    /// </summary>
    public class CloseBrowser : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "CloseBrowser";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override int Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            seleniumDriver.CloseBrowser();
            return 0;
        }
    }
}
