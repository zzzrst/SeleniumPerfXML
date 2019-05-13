// <copyright file="ContentDownloader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using System.Net;

    /// <summary>
    /// Get resources content from URLs.
    /// </summary>
    internal class ContentDownloader : IContentDownloader
    {
        /// <summary>
        /// Defines the webClient.
        /// </summary>
        private readonly WebClient webClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDownloader"/> class.
        /// </summary>
        /// <param name="webClient">WebClient instace to use.</param>
        public ContentDownloader(WebClient webClient)
        {
            if (webClient == null)
            {
                throw new ArgumentNullException(nameof(webClient));
            }

            this.webClient = webClient;
        }

        /// <summary>
        /// Get the resource's content.
        /// </summary>
        /// <param name="resourceUrl">Resource url.</param>
        /// <returns>Content of the resource.</returns>
        public string GetContent(Uri resourceUrl)
        {
            var contentString = this.webClient.DownloadString(resourceUrl);
            return contentString;
        }
    }
}
