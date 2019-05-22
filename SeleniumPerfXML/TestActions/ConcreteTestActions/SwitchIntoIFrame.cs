// <copyright file="SwitchIntoIFrame.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Xml;

    /// <summary>
    /// This class executes the action of switching context into IFrame.
    /// </summary>
    public class SwitchIntoIFrame : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "SwitchIntoIFrame";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            string xPath = testActionInformation.Attributes["xPath"].Value;
            seleniumDriver.SwitchToIFrame(xPath);
        }
    }
}
