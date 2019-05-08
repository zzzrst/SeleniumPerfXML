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
        /// Gets or sets a value indicating whether to log this action or not.
        /// </summary>
        public bool Log { get; set; }

        /// <summary>
        /// Gets or sets the name of the test action.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to perform this action or not.
        /// </summary>
        public bool PerformAction { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to run AODA or not.
        /// </summary>
        public bool RunAODA { get; set; }

        /// <summary>
        /// Gets or sets the page name to use when running AODA.
        /// </summary>
        public string RunAODAPageName { get; set; }

        /// <summary>
        /// Gets or sets the test action XML Node.
        /// </summary>
        public XmlNode TestActionInformation { get; set; }

        /// <summary>
        /// Executes the test action based on the provided information and settings.
        /// </summary>
        public abstract void Execute();
    }
}
