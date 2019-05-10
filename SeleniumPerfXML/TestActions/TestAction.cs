// <copyright file="TestAction.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Abstract class for Test Actions to be implemented.
    /// </summary>
    public abstract class TestAction
    {
        /// <summary>
        /// Gets or sets the Description
        /// Description mandatory set EXACTLY to test actions set in XML in child classes for reflection to work.
        /// </summary>
        public abstract string Description { get; protected set; }

        /// <summary>
        /// Executes the test action based on the provided information and settings.
        /// </summary>
        /// <param name="log"> Indicates whether to log this test case or not.</param>
        /// <param name="name"> The test step name for this action. </param>
        /// <param name="performAction"> Indicates whether to perform this action or not.</param>
        /// <param name="runAODA"> Indicates whether to run AODA scripts after this action. </param>
        /// <param name="runAODAPageName"> Indicates the page name to pass to AODA scripts. </param>
        /// <param name="testActionInformation"> Provides the XML information for this test action. </param>
        /// <param name="seleniumDriver"> Passes on the driver instance used to interact with the browser. </param>
        public abstract void Execute(
            bool log,
            string name,
            bool performAction,
            bool runAODA,
            string runAODAPageName,
            XmlNode testActionInformation,
            SeleniumDriver seleniumDriver);
    }
}
