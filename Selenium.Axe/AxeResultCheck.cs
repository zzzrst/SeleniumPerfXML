// <copyright file="AxeResultCheck.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Selenium.Axe
{
    /// <summary>
    /// Defines the <see cref="AxeResultCheck" />.
    /// </summary>
    public class AxeResultCheck
    {
        /// <summary>
        /// Gets or sets the Data.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Impact.
        /// </summary>
        public string Impact { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the RelatedNodes.
        /// </summary>
        public AxeResultRelatedNode[] RelatedNodes { get; set; }
    }
}
