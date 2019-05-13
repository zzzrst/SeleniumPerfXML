// <copyright file="ClickElementByXPath.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// This class executes the action of clicking an element by xpath.
    /// </summary>
    public class ClickElementByXPath : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "ClickElementByXPath";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver)
        {
            string xPath = testActionInformation.Attributes["xPath"].Value;
            bool useJS = false;
            if (testActionInformation.Attributes["useJS"] != null)
            {
                useJS = bool.Parse(testActionInformation.Attributes["useJS"].Value);
            }

            seleniumDriver.ClickElement(xPath, useJS);
            seleniumDriver.WaitForLoadingSpinner();
        }
    }
}
