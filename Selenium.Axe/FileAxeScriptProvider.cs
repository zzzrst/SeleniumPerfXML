// <copyright file="FileAxeScriptProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="FileAxeScriptProvider" />.
    /// </summary>
    public class FileAxeScriptProvider : IAxeScriptProvider
    {
        /// <summary>
        /// Defines the filePath.
        /// </summary>
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAxeScriptProvider"/> class.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        public FileAxeScriptProvider(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exists", nameof(filePath));
            }

            this.filePath = filePath;
        }

        /// <summary>
        /// The GetScript.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetScript()
        {
            if (!File.Exists(this.filePath))
            {
                throw new InvalidOperationException($"File '{this.filePath}' does not exist");
            }

            return File.ReadAllText(this.filePath);
        }
    }
}
