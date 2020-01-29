// <copyright file="ChooseCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Implementations
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of getting to an organization.
    /// </summary>
    public class ChooseCollection : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "ChooseCollection";

        /// <inheritdoc/>
        public override void Execute()
        {
            string collectionSearchField = this.TestStepInfo.Attributes["collectionSearchField"].Value;
            string collectionName = this.TestStepInfo.Attributes["collectionName"].Value;

            string collectionDropDown = "//*[@aria-label='Choose a collection activate']";
            string collectionSearchBarXPath = "//*[@aria-label='Choose a collection']";
            string collectionElementXpath = $"//*[contains(text(), \"{collectionName}\")]";

            this.Driver.RefreshWebPage();

            if (!this.Driver.CheckForElementState(collectionElementXpath, SeleniumDriver.ElementState.Visible))
            {
                this.Driver.ClickElement(collectionDropDown);
                this.Driver.PopulateElement(collectionSearchBarXPath, collectionSearchField);
                this.Driver.ClickElement(collectionElementXpath);
                this.Driver.WaitForLoadingSpinner();
            }
        }
    }
}
