// <copyright file="SignIn.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System.Xml;

    /// <summary>
    /// This class executes the action of signing in.
    /// </summary>
    public class SignIn : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "SignIn";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver)
        {
            string usernameXPath = "//input[@id='username']";
            string username = testActionInformation.Attributes["username"].Value;

            string passwordXPath = "//input[@id='password']";
            string password = testActionInformation.Attributes["password"].Value;

            string signInButtonXPath = "//input[@id='signin']";

            seleniumDriver.NavigateToURL();
            seleniumDriver.PopulateElement(usernameXPath, username);
            seleniumDriver.PopulateElement(passwordXPath, password);
            seleniumDriver.ClickElement(signInButtonXPath);
            seleniumDriver.WaitForLoadingSpinner();
        }
    }
}
