// <copyright file="WebDriverExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using OpenQA.Selenium;

    /// <summary>
    /// Defines the <see cref="WebDriverExtensions" />.
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// The Analyze.
        /// </summary>
        /// <param name="webDriver">The webDriver<see cref="IWebDriver"/>.</param>
        /// <returns>The <see cref="AxeResult"/>.</returns>
        public static AxeResult Analyze(this IWebDriver webDriver)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException(nameof(webDriver));
            }

            AxeBuilder axeBuilder = new AxeBuilder(webDriver);
            return axeBuilder.Analyze();
        }

        /// <summary>
        /// The Analyze.
        /// </summary>
        /// <param name="webDriver">The webDriver<see cref="IWebDriver"/>.</param>
        /// <param name="axeBuilderOptions">The axeBuilderOptions<see cref="AxeBuilderOptions"/>.</param>
        /// <returns>The <see cref="AxeResult"/>.</returns>
        public static AxeResult Analyze(this IWebDriver webDriver, AxeBuilderOptions axeBuilderOptions)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException(nameof(webDriver));
            }

            AxeBuilder axeBuilder = new AxeBuilder(webDriver, axeBuilderOptions);
            return axeBuilder.Analyze();
        }

        /// <summary>
        /// The Analyze.
        /// </summary>
        /// <param name="webDriver">The webDriver<see cref="IWebDriver"/>.</param>
        /// <param name="context">The context<see cref="IWebElement"/>.</param>
        /// <returns>The <see cref="AxeResult"/>.</returns>
        public static AxeResult Analyze(this IWebDriver webDriver, IWebElement context)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException(nameof(webDriver));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            AxeBuilder axeBuilder = new AxeBuilder(webDriver);
            return axeBuilder.Analyze(context);
        }

        /// <summary>
        /// The Analyze.
        /// </summary>
        /// <param name="webDriver">The webDriver<see cref="IWebDriver"/>.</param>
        /// <param name="context">The context<see cref="IWebElement"/>.</param>
        /// <param name="axeBuilderOptions">The axeBuilderOptions<see cref="AxeBuilderOptions"/>.</param>
        /// <returns>The <see cref="AxeResult"/>.</returns>
        public static AxeResult Analyze(this IWebDriver webDriver, IWebElement context, AxeBuilderOptions axeBuilderOptions)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException(nameof(webDriver));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            AxeBuilder axeBuilder = new AxeBuilder(webDriver, axeBuilderOptions);
            return axeBuilder.Analyze(context);
        }
    }
}
