// <copyright file="PopulateElementByXPath.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of populating the element identified by the xpath.
    /// </summary>
    public class PopulateElementByXPath : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "PopulateElementByXPath";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver)
        {
            string xPath = testActionInformation.Attributes["xPath"].Value;
            string text = testActionInformation.Attributes["text"].Value;
            seleniumDriver.PopulateElement(xPath, text);
            seleniumDriver.WaitForLoadingSpinner();
        }
    }
}
