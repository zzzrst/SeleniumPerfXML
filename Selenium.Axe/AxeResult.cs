// <copyright file="AxeResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Defines the <see cref="AxeResult" />.
    /// </summary>
    public class AxeResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AxeResult"/> class.
        /// </summary>
        /// <param name="result">The result<see cref="T:JObject"/>.</param>
        public AxeResult(JObject result)
        {
            JToken results = result.SelectToken("results");
            JToken violationsToken = results.SelectToken("violations");
            JToken passesToken = results.SelectToken("passes");
            JToken inapplicableToken = results.SelectToken("inapplicable");
            JToken incompleteToken = results.SelectToken("incomplete");
            JToken timestampToken = results.SelectToken("timestamp");
            JToken urlToken = results.SelectToken("url");
            JToken error = result.SelectToken("error");

            this.Violations = violationsToken?.ToObject<AxeResultItem[]>();
            this.Passes = passesToken?.ToObject<AxeResultItem[]>();
            this.Inapplicable = inapplicableToken?.ToObject<AxeResultItem[]>();
            this.Incomplete = incompleteToken?.ToObject<AxeResultItem[]>();
            this.Timestamp = timestampToken?.ToObject<DateTimeOffset>();
            this.Url = urlToken?.ToObject<string>();
            this.Error = error?.ToObject<string>();
        }

        /// <summary>
        /// Gets the Violations.
        /// </summary>
        public AxeResultItem[] Violations { get; }

        /// <summary>
        /// Gets the Passes.
        /// </summary>
        public AxeResultItem[] Passes { get; }

        /// <summary>
        /// Gets the Inapplicable.
        /// </summary>
        public AxeResultItem[] Inapplicable { get; }

        /// <summary>
        /// Gets the Incomplete.
        /// </summary>
        public AxeResultItem[] Incomplete { get; }

        /// <summary>
        /// Gets the Timestamp.
        /// </summary>
        public DateTimeOffset? Timestamp { get; private set; }

        /// <summary>
        /// Gets the Url.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the Error.
        /// </summary>
        public string Error { get; private set; }
    }
}
