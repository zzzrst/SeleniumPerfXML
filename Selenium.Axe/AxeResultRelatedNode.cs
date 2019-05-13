// <copyright file="AxeResultRelatedNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AxeResultRelatedNode" />.
    /// </summary>
    public class AxeResultRelatedNode
    {
        /// <summary>
        /// Gets or sets the Html.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets the Target.
        /// </summary>
        public List<string> Target { get; set; }
    }
}
