// <copyright file="IAccessibilityChecker.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Axe
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenQA.Selenium;

    /// <summary>
    /// The Accessibility interface.
    /// </summary>
    public interface IAccessibilityChecker
    {
        /// <summary>
        /// Gets or sets the webdriver variable.
        /// </summary>
        public IWebDriver WebDriver { get; set; }
    }
}
