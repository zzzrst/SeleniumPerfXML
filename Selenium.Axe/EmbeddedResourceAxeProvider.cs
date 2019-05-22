// <copyright file="EmbeddedResourceAxeProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Defines the <see cref="EmbeddedResourceAxeProvider" />.
    /// </summary>
    internal class EmbeddedResourceAxeProvider : IAxeScriptProvider
    {
        /// <summary>
        /// The GetScript.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetScript() => File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Resources\\axe.min.js");
    }
}
