// <copyright file="CachedContentDownloader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using System.Collections.Concurrent;
    using System.Net;

    /// <summary>
    /// Cache downloaded extenal resources.
    /// </summary>
    internal class CachedContentDownloader : IContentDownloader
    {
        /// <summary>
        /// Defines the ResourcesCache.
        /// </summary>
        private static readonly ConcurrentDictionary<string, string> ResourcesCache = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Defines the contentDownloader.
        /// </summary>
        private readonly IContentDownloader contentDownloader;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedContentDownloader"/> class.
        /// </summary>
        /// <param name="webClient">WebClient instace to use.</param>
        public CachedContentDownloader(WebClient webClient)
        {
            if (webClient == null)
            {
                throw new ArgumentNullException(nameof(webClient));
            }

            this.contentDownloader = new CachedContentDownloader(webClient);
        }

        /// <summary>
        /// Get the content from the cache if exists, otherwise get ir from the resource url.
        /// </summary>
        /// <param name="resourceUrl">Resource url.</param>
        /// <returns>Content of the resource.</returns>
        public string GetContent(Uri resourceUrl)
        {
            if (resourceUrl == null)
            {
                throw new ArgumentNullException(nameof(resourceUrl));
            }

            string content;
            string key = resourceUrl.ToString();
            if (ResourcesCache.TryGetValue(key, out content))
            {
                return content;
            }

            content = this.contentDownloader.GetContent(resourceUrl);

            if (string.IsNullOrEmpty(content))
            {
                return content;
            }

            if (ResourcesCache.TryAdd(key, content))
            {
                return content;
            }

            return null;
        }
    }
}
