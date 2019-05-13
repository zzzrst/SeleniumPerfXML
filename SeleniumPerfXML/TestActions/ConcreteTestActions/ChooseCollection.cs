// <copyright file="ChooseCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of getting to an organization.
    /// </summary>
    public class ChooseCollection : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "ChooseCollection";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver)
        {
            string collectionSearchField = testActionInformation.Attributes["collectionSearchField"].Value;
            string collectionName = testActionInformation.Attributes["collectionName"].Value;

            string collectionDropDown = "//*[@aria-label='Choose a collection activate']";
            string collectionSearchBarXPath = "//*[@aria-label='Choose a collection']";
            string collectionElementXpath = $"//*[contains(text(), \"{collectionName}\")]";

            seleniumDriver.RefreshWebPage();

            if (!seleniumDriver.CheckForElementState(collectionElementXpath, SeleniumDriver.ElementState.Visible))
            {
                seleniumDriver.ClickElement(collectionDropDown);
                seleniumDriver.PopulateElement(collectionSearchBarXPath, collectionSearchField);
                seleniumDriver.ClickElement(collectionElementXpath);
                seleniumDriver.WaitForLoadingSpinner();
            }
        }
    }
}
