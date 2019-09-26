// <copyright file="DDLSelectByXPath.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of selecting a value from the specified dropdownlist.
    /// </summary>
    public class DDLSelectByXPath : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "DDLSelectByXPath";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override int Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            string xPath = testActionInformation.Attributes["xPath"].Value;
            string selection = testActionInformation.Attributes["selection"].Value;
            seleniumDriver.SelectValueInElement(xPath, selection);
            seleniumDriver.WaitForLoadingSpinner();
            return 0;
        }
    }
}
