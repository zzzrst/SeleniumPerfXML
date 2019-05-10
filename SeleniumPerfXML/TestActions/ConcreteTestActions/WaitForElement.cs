﻿// <copyright file="WaitForElement.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// This class executes the action of waiting for an elment's status.
    /// </summary>
    public class WaitForElement : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "WaitForElement";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver)
        {
            throw new NotImplementedException();
        }
    }
}
