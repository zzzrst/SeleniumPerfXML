// <copyright file="IContentDownloader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;

    /// <summary>
    /// Defines the <see cref="IContentDownloader" />.
    /// </summary>
    internal interface IContentDownloader
    {
        /// <summary>
        /// Get the resource's content.
        /// </summary>
        /// <param name="resourceUrl">Resource url.</param>
        /// <returns>Content of the resource.</returns>
        string GetContent(Uri resourceUrl);
    }
}
