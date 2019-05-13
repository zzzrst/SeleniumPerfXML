// <copyright file="IncludeExcludeManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Handle all initialization, serialization and validations for includeExclude aXe object.
    /// For more info check this: https://github.com/dequelabs/axe-core/blob/master/doc/API.md#include-exclude-object.
    /// </summary>
    public class IncludeExcludeManager
    {
        /// <summary>
        /// Defines the JsonSerializerSettings.
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };

        /// <summary>
        /// Defines the includeList.
        /// </summary>
        private List<string[]> includeList;

        /// <summary>
        /// Defines the excludeList.
        /// </summary>
        private List<string[]> excludeList;

        /// <summary>
        /// Include the given selectors, i.e "#foo", "ul.bar .target", "div".
        /// </summary>
        /// <param name="selectors">Selectors to include.</param>
        public void Include(params string[] selectors)
        {
            ValidateParameters(selectors);
            if (this.includeList == null)
            {
                this.includeList = new List<string[]>();
            }

            this.includeList.Add(selectors);
        }

        /// <summary>
        /// Include the given selectors, i.e "frame", "div.foo".
        /// </summary>
        /// <param name="selectors">Selectors to exclude.</param>
        public void Exclude(params string[] selectors)
        {
            ValidateParameters(selectors);
            if (this.excludeList == null)
            {
                this.excludeList = new List<string[]>();
            }

            this.excludeList.Add(selectors);
        }

        /// <summary>
        /// Indicate if we have more than one entry on include list or we have entries on exclude list.
        /// </summary>
        /// <returns>True or False.</returns>
        public bool HasMoreThanOneSelectorsToIncludeOrSomeToExclude()
        {
            bool hasMoreThanOneSelectorsToInclude = this.includeList != null && this.includeList.Count > 1;
            bool hasSelectorsToExclude = this.excludeList != null && this.excludeList.Count > 0;

            return hasMoreThanOneSelectorsToInclude || hasSelectorsToExclude;
        }

        /// <summary>
        /// Indicate we have one entry on the include list.
        /// </summary>
        /// <returns>True or False.</returns>
        public bool HasOneItemToInclude() => this.includeList != null && this.includeList.Count == 1;

        /// <summary>
        /// Get first selector of the first entry on include list.
        /// </summary>
        /// <returns>Returns the first selector of the first entry on include list.</returns>
        public string GetFirstItemToInclude()
        {
            if (this.includeList == null || this.includeList.Count == 0)
            {
                throw new InvalidOperationException("You must add at least one selector to include");
            }

            return this.includeList.First().First();
        }

        /// <summary>
        /// Serialize this instance on JSON format.
        /// </summary>
        /// <returns>This instance serialized in JSON format.</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, JsonSerializerSettings);
        }

        /// <summary>
        /// The ValidateParameters.
        /// </summary>
        /// <param name="selectors">The selectors<see cref="T:string[]"/>.</param>
        private static void ValidateParameters(string[] selectors)
        {
            if (selectors == null)
            {
                throw new ArgumentNullException(nameof(selectors));
            }

            if (selectors.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException("There is some items null or empty", nameof(selectors));
            }
        }
    }
}
