// <copyright file="WaitInSeconds.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This class executes the action of waiting for x seconds.
    /// </summary>
    public class WaitInSeconds : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "WaitInSeconds";

        /// <inheritdoc/>
        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
