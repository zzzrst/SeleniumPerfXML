// <copyright file="AxeResultNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AxeResultNode" />.
    /// </summary>
    public class AxeResultNode
    {
        /// <summary>
        /// Gets or sets the Target.
        /// </summary>
        public List<string> Target { get; set; }

        /// <summary>
        /// Gets or sets the Html.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets the Impact.
        /// </summary>
        public string Impact { get; set; }

        /// <summary>
        /// Gets or sets the Any.
        /// </summary>
        public AxeResultCheck[] Any { get; set; }

        /// <summary>
        /// Gets or sets the All.
        /// </summary>
        public AxeResultCheck[] All { get; set; }

        /// <summary>
        /// Gets or sets the None.
        /// </summary>
        public AxeResultCheck[] None { get; set; }
    }
}
