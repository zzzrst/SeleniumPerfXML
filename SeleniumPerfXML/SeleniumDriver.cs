// <copyright file="SeleniumDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Driver class for Selenium WebDriver.
    /// </summary>
    public class SeleniumDriver
    {
        /// <summary>
        /// Location of the Selenium drivers on the current machine.
        /// </summary>
        private readonly string seleniumDriverLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private IWebDriver webDriver;
        private WebDriverWait wdWait;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeleniumDriver"/> class.
        /// </summary>
        /// <param name="browserType">The browser to use for this driver. </param>
        /// <param name="timeOutThreshold"> The timeout threshold in seconds.</param>
        public SeleniumDriver(Browser browserType, TimeSpan timeOutThreshold)
        {
            TimeSpan actualTimeOut = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["ActualTimeOut"]));

            switch (browserType)
            {
                case Browser.Chrome:

                    ChromeOptions chromeOptions = new ChromeOptions
                    {
                        UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                    };

                    chromeOptions.AddArgument("no-sandbox");
                    chromeOptions.AddArgument("--log-level=3");
                    chromeOptions.AddArgument("--silent");
                    chromeOptions.BinaryLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\chromium\\chrome.exe";

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService(this.seleniumDriverLocation);
                    service.SuppressInitialDiagnosticInformation = true;

                    this.webDriver = new ChromeDriver(this.seleniumDriverLocation, chromeOptions, actualTimeOut);

                    break;
                case Browser.Edge:

                    this.webDriver = new EdgeDriver(this.seleniumDriverLocation, null, actualTimeOut);

                    break;
                case Browser.Firefox:

                    this.webDriver = new FirefoxDriver(this.seleniumDriverLocation, null, actualTimeOut);

                    break;
                case Browser.IE:

                    InternetExplorerOptions ieOptions = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        IgnoreZoomLevel = true,
                        EnsureCleanSession = true,
                        EnableNativeEvents = bool.Parse(ConfigurationManager.AppSettings["IEEnableNativeEvents"].ToString()),
                        UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                        RequireWindowFocus = true,
                    };
                    InternetExplorerDriverService ieService = InternetExplorerDriverService.CreateDefaultService(this.seleniumDriverLocation);
                    ieService.SuppressInitialDiagnosticInformation = true;
                    this.webDriver = new InternetExplorerDriver(ieService, ieOptions, actualTimeOut);

                    break;
                case Browser.Safari:

                    Logger.Info("We currently do not deal with Safari yet!");

                    break;
            }

            this.wdWait = new WebDriverWait(this.webDriver, timeOutThreshold);
        }

        /// <summary>
        /// Different browsers that are supported.
        /// </summary>
        public enum Browser
        {
            /// <summary>
            /// Represents the Chrome browser.
            /// </summary>
            Chrome,

            /// <summary>
            /// Represents the Microsoft Edge Browser.
            /// </summary>
            Edge,

            /// <summary>
            /// Represents the Firefox Browser.
            /// </summary>
            Firefox,

            /// <summary>
            /// Represents the Internet Explorer browser.
            /// </summary>
            IE,

            /// <summary>
            /// Represents the Safari Browser.
            /// </summary>
            Safari,
        }

        /// <summary>
        /// Different states of the element.
        /// </summary>
        public enum ElementState
        {
            /// <summary>
            /// Element cannot be found / seen.
            /// </summary>
            Invisible,

            /// <summary>
            /// Element can be seen.
            /// </summary>
            Visible,

            /// <summary>
            /// Element can be clicked.
            /// </summary>
            Clickable,
        }

        /// <summary>
        /// Gets the url of the page the webdriver is focued on.
        /// </summary>
        public string CurrentURL => this.webDriver.Url;

        /// <summary>
        /// Gets or sets the loadiong spinner that appears on the website.
        /// </summary>
        public string LoadingSpinner { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the error container to check if any errors are shown on the UI.
        /// </summary>
        public string ErrorContainer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the web driver is in an IFrame or not.
        /// </summary>
        private bool InIFrame { get; set; } = false;

        /// <summary>
        /// Checks for an element state.
        /// </summary>
        /// <param name="xPath"> The xpath to find the web element. </param>
        /// <param name="state"> The state of the web element to wait for. </param>
        /// <returns> If the element state is as wanted.</returns>
        public bool CheckForElementState(string xPath, ElementState state)
        {
            IWebElement element = null;

            try
            {
                this.GetElementByXPath(xPath);
            }
            catch (NoSuchElementException)
            {
                // this is expected if we are checking that it is not visible.
            }
            catch (Exception e)
            {
               Logger.Error(e.ToString());
            }

            switch (state)
            {
                case ElementState.Invisible:
                    return element == null;

                case ElementState.Visible:
                    return element != null;

                case ElementState.Clickable:
                    return element != null && element.Displayed && element.Enabled;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Performs the actions of clicking the specified element. Uses Selenium binding by default.
        /// </summary>
        /// <param name="xPath">The xpath to find the specified element.</param>
        /// <param name="byJS"> Whether to use JS to perform the click / not. </param>
        public void ClickElement(string xPath, bool byJS = false)
        {
            IWebElement element = this.GetElementByXPath(xPath);
            this.wdWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            if (byJS)
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)this.webDriver;
                executor.ExecuteScript("var element=arguments[0]; setTimeout(function() {element.click();}, 100)", element);
            }
            else
            {
                element.Click();
            }
        }

        /// <summary>
        /// Closes the current window. It will quite the browser if it is the last window opened.
        /// </summary>
        public void CloseBrowser()
        {
            this.webDriver.Close();
        }

        /// <summary>
        /// Tells the browser to navigate to the provided url.
        /// </summary>
        /// <param name="url">URL for the browser to navigate to.</param>
        /// <returns> <code>true</code> if the navigation was successful. </returns>
        public bool NavigateToURL(string url)
        {
            try
            {
                this.webDriver.Url = url;
                return true;
            }
            catch (Exception e)
            {
                Logger.Error($"Something went wrong while navigating to url: {e.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// Performs the action of populating a value.
        /// </summary>
        /// <param name="xPath"> The xpath to use to identify the element. </param>
        /// <param name="value"> The value to populate.</param>
        public void PopulateElementByXPath(string xPath, string value)
        {
            IWebElement element = this.GetElementByXPath(xPath);
            this.wdWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            element.SendKeys(value);
        }

        /// <summary>
        /// Performs the action of selecting a value in an element.
        /// </summary>
        /// <param name="xPath"> The xpath to use to identify the element. </param>
        /// <param name="value"> The value to select in the element.</param>
        public void SelectValueInElement(string xPath, string value)
        {
            IWebElement ddlElement = this.GetElementByXPath(xPath);
            SelectElement ddl = new SelectElement(ddlElement);
            this.wdWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ddlElement));
            ddl.SelectByText(value);
            ddlElement.Click();
        }

        /// <summary>
        /// Switches to appropriate IFrame.
        /// </summary>
        /// <param name="xPath"> xPath to find the iFrame.</param>
        public void SwitchToIFrame(string xPath)
        {
            this.SetActiveTab();
            this.webDriver.SwitchTo().DefaultContent();

            if (xPath == "root")
            {
                this.InIFrame = false;
            }
            else
            {
                this.wdWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath(xPath)));
                this.InIFrame = true;
            }
        }

        /// <summary>
        /// The SwitchToTab.
        /// </summary>
        /// <param name="tab">The tab<see cref="int"/>.</param>
        public void SwitchToTab(int tab)
        {
            var tabs = this.webDriver.WindowHandles;
            this.webDriver.SwitchTo().Window(tabs[tab]);
        }

        /// <summary>
        /// Waits for an element state.
        /// </summary>
        /// <param name="xPath"> The xpath to find the web element. </param>
        /// <param name="state"> The state of the web element to wait for. </param>
        public void WaitForElementState(string xPath, ElementState state)
        {
            switch (state)
            {
                case ElementState.Invisible:

                    this.wdWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xPath)));
                    break;

                case ElementState.Visible:
                    this.wdWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xPath)));
                    break;

                case ElementState.Clickable:
                    this.wdWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(xPath)));
                    break;
            }
        }

        /// <summary>
        /// Dismisses the alert dialog box on the browser.
        /// </summary>
        private void ClickOk()
        {
            try
            {
                this.webDriver.SwitchTo().Alert().Dismiss();
            }
            catch (Exception)
            {
            }
        }

        private void CheckErrorContainer()
        {
            if (this.ErrorContainer != string.Empty)
            {
                try
                {
                    IWebElement errorContainer = this.webDriver.FindElement(By.XPath(this.ErrorContainer));
                    Logger.Error($"Found the following in the error container: {errorContainer.Text}");
                }
                catch (Exception)
                {
                    // we do nothing if we don't find it.
                }
            }
        }

        /// <summary>
        /// Finds the first IWebElement By XPath.
        /// </summary>
        /// <param name="xPath">The xpath to find the web element.</param>
        /// <returns> The first IWebElement whose xpath matches. </returns>
        private IWebElement GetElementByXPath(string xPath)
        {
            this.WaitForLoadingSpinner();
            return this.wdWait.Until(driver => driver.FindElement(By.XPath(xPath)));
        }

        /// <summary>
        /// Waits until the loading spinner disappears. PR applications only.
        /// </summary>
        private void WaitForLoadingSpinner()
        {
            try
            {
                this.SetActiveTab();

                if (this.LoadingSpinner != string.Empty)
                {
                    this.wdWait.Until(
                        SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(
                            By.XPath(this.LoadingSpinner)));
                }
            }
            catch (Exception)
            {
                // we want to do nothing here
            }
        }

        /// <summary>
        /// Sets the tab to be the active one.
        /// </summary>
        private void SetActiveTab()
        {
            if (!this.InIFrame)
            {
                var windows = this.webDriver.WindowHandles;
                int windowCount = windows.Count;
                this.webDriver.SwitchTo().Window(windows[windowCount - 1]);
            }
        }
    }
}
