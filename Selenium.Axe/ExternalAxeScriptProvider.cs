// <copyright file="ExternalAxeScriptProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using System.Net;

    /// <summary>
    /// Defines the <see cref="ExternalAxeScriptProvider" />.
    /// </summary>
    public class ExternalAxeScriptProvider : IAxeScriptProvider
    {
        /// <summary>
        /// Defines the scriptUri.
        /// </summary>
        private readonly Uri scriptUri;

        /// <summary>
        /// Defines the contentDownloader.
        /// </summary>
        private readonly IContentDownloader contentDownloader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalAxeScriptProvider"/> class.
        /// </summary>
        /// <param name="webClient">The webClient<see cref="WebClient"/>.</param>
        /// <param name="scriptUri">The scriptUri<see cref="Uri"/>.</param>
        public ExternalAxeScriptProvider(WebClient webClient, Uri scriptUri)
        {
            if (webClient == null)
            {
                throw new ArgumentNullException(nameof(webClient));
            }

            if (scriptUri == null)
            {
                throw new ArgumentNullException(nameof(scriptUri));
            }

            this.scriptUri = scriptUri;
            this.contentDownloader = new CachedContentDownloader(webClient);
        }

        /// <summary>
        /// The GetScript.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetScript() => this.contentDownloader.GetContent(this.scriptUri);
    }
}
