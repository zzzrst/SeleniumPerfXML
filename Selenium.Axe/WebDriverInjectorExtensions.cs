// <copyright file="WebDriverInjectorExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using OpenQA.Selenium;

    /// <summary>
    /// Defines the <see cref="WebDriverInjectorExtensions" />.
    /// </summary>
    internal static class WebDriverInjectorExtensions
    {
        /// <summary>
        /// Injects Axe script into frames.
        /// </summary>
        /// <param name="driver">WebDriver instance to inject into.</param>
        /// <param name="scriptProvider">Provider that get the aXe script to inject.</param>
        internal static void Inject(this IWebDriver driver, IAxeScriptProvider scriptProvider)
        {
            if (scriptProvider == null)
            {
                throw new ArgumentNullException(nameof(scriptProvider));
            }

            string script = scriptProvider.GetScript();

            // try to insert script 5 times.
            int tries = 0;
            bool succeed = false;
            while (!succeed && tries < 5)
            {
                try
                {
                    IList<IWebElement> parents = new List<IWebElement>();
                    InjectIntoFrames(driver, script, parents);
                }
                catch (StaleElementReferenceException)
                {
                    tries++;
                }

                succeed = true;
            }

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            driver.SwitchTo().DefaultContent();
            if (succeed)
            {
                Console.WriteLine("Running AODA script");
                js.ExecuteScript(script);
            }
        }

        /// <summary>
        /// Recursively find frames and inject a script into them.
        /// </summary>
        /// <param name="driver">An initialized WebDriver.</param>
        /// <param name="script">Script to inject.</param>
        /// <param name="parents">A list of all toplevel frames.</param>
        private static void InjectIntoFrames(IWebDriver driver, string script, IList<IWebElement> parents)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            IList<IWebElement> frames = driver.FindElements(By.TagName("iframe"));

            foreach (var frame in frames)
            {
                driver.SwitchTo().DefaultContent();

                if (parents != null)
                {
                    foreach (IWebElement parent in parents)
                    {
                        driver.SwitchTo().Frame(parent);
                    }
                }

                driver.SwitchTo().Frame(frame);
                js.ExecuteScript(script);

                IList<IWebElement> localParents = parents.ToList();
                localParents.Add(frame);

                InjectIntoFrames(driver, script, localParents);
            }
        }
    }
}
