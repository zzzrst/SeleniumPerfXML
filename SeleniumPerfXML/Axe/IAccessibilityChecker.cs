// <copyright file="IAccessibilityChecker.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxeAccessibilityDriver
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

        /// <summary>
        /// This captures the AODA result for this webpage.
        /// </summary>
        /// <param name="providedPageTitle"> Title of the page. </param>
        public void CaptureResults(string providedPageTitle);

        /// <summary>
        /// Logs the result for this file.
        /// </summary>
        /// <param name="folderLocation">Location to save all the results.</param>
        public void LogResults(string folderLocation);
    }

}
