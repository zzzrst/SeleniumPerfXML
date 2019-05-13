// <copyright file="AxeResultItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    /// <summary>
    /// Defines the <see cref="AxeResultItem" />.
    /// </summary>
    public class AxeResultItem
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Help.
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Gets or sets the HelpUrl.
        /// </summary>
        public string HelpUrl { get; set; }

        /// <summary>
        /// Gets or sets the Impact.
        /// </summary>
        public string Impact { get; set; }

        /// <summary>
        /// Gets or sets the Tags.
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Gets or sets the Nodes.
        /// </summary>
        public AxeResultNode[] Nodes { get; set; }
    }
}
