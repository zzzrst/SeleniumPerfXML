// <copyright file="AxeBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using Newtonsoft.Json.Linq;
    using OpenQA.Selenium;

    /// <summary>
    /// Fluent style builder for invoking aXe. Instantiate a new Builder and configure testing with the include(),
    /// exclude(), and options() methods before calling analyze() to run.
    /// </summary>
    public class AxeBuilder
    {
        /// <summary>
        /// Defines the DefaultOptions.
        /// </summary>
        private static readonly AxeBuilderOptions DefaultOptions = new AxeBuilderOptions { ScriptProvider = new EmbeddedResourceAxeProvider() };

        /// <summary>
        /// Defines the webDriver.
        /// </summary>
        private readonly IWebDriver webDriver;

        /// <summary>
        /// Defines the includeExcludeManager.
        /// </summary>
        private readonly IncludeExcludeManager includeExcludeManager = new IncludeExcludeManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="AxeBuilder"/> class.
        /// </summary>
        /// <param name="webDriver">Selenium driver to use.</param>
        public AxeBuilder(IWebDriver webDriver)
            : this(webDriver, DefaultOptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AxeBuilder"/> class.
        /// </summary>
        /// <param name="webDriver">Selenium driver to use.</param>
        /// <param name="options">Builder options.</param>
        public AxeBuilder(IWebDriver webDriver, AxeBuilderOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.webDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            this.webDriver.Inject(options.ScriptProvider);
        }

        /// <summary>
        /// Gets or sets the Options.
        /// </summary>
        public string Options { get; set; } = "{}";

        /// <summary>
        /// Selectors to include in the validation.
        /// </summary>
        /// <param name="selectors">Any valid CSS selectors.</param>
        /// <returns>AxeBuilder with Included selectors.</returns>
        public AxeBuilder Include(params string[] selectors)
        {
            this.includeExcludeManager.Include(selectors);
            return this;
        }

        /// <summary>
        /// Exclude selectors
        /// Selectors to exclude in the validation.
        /// </summary>
        /// <param name="selectors">Any valid CSS selectors.</param>
        /// <returns>AxeBuilder with Excluded selectors.</returns>
        public AxeBuilder Exclude(params string[] selectors)
        {
            this.includeExcludeManager.Exclude(selectors);
            return this;
        }

        /// <summary>
        /// Run aXe against a specific WebElement.
        /// </summary>
        /// <param name="context"> A WebElement to test.</param>
        /// <returns>An aXe results document.</returns>
        public AxeResult Analyze(IWebElement context)
        {
            // string command = string.Format("axe.a11yCheck(arguments[0], {0}, arguments[arguments.length - 1]);", Options);
            string command = this.GetAxeSnippet("arguments[0]");
            return this.Execute(command, context);
        }

        /// <summary>
        /// Run aXe against the page.
        /// </summary>
        /// <returns>An aXe results document.</returns>
        public AxeResult Analyze()
        {
            string axeContext;

            if (this.includeExcludeManager.HasMoreThanOneSelectorsToIncludeOrSomeToExclude())
            {
                axeContext = $"{this.includeExcludeManager.ToJson()}";
            }
            else if (this.includeExcludeManager.HasOneItemToInclude())
            {
                string itemToInclude = this.includeExcludeManager.GetFirstItemToInclude().Replace("'", string.Empty);
                axeContext = $"{itemToInclude}";
            }
            else
            {
                axeContext = "document";
            }

            string command = this.GetAxeSnippet(axeContext);

            return this.Execute(command);
        }

        /// <summary>
        /// Execute the script into the target.
        /// </summary>
        /// <param name="command">Script to execute.</param>
        /// <param name="args">Arguments for the script.</param>
        /// <returns>The <see cref="AxeResult"/>.</returns>
        private AxeResult Execute(string command, params object[] args)
        {
            object response = ((IJavaScriptExecutor)this.webDriver).ExecuteAsyncScript(command, args);
            var jObject = JObject.FromObject(response);
            return new AxeResult(jObject);
        }

        /// <summary>
        /// The getAxeSnippet.
        /// </summary>
        /// <param name="context">The context<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetAxeSnippet(string context)
        {
            return string.Format(
                "var callback = arguments[arguments.length - 1];" +
                "var context = {0};" +
                "var options = {1};" +
                "var result = {{ error: '', results: null }};" +
                "axe.run(context, options, function (err, res) {{" +
                "  if (err) {{" +
                "    result.error = err.message;" +
                "  }} else {{" +
                "    result.results = res;" +
                "  }}" +
                "  callback(result);" +
                "}});",
                context,
                $"{this.Options}");
        }
    }
}
