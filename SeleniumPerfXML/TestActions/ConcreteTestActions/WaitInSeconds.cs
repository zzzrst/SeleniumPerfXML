// <copyright file="WaitInSeconds.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using System.Threading;
    using System.Xml;

    /// <summary>
    /// This class executes the action of waiting for x seconds.
    /// </summary>
    public class WaitInSeconds : TestAction
    {
        /// <inheritdoc/>
        public override string Description { get; protected set; } = "WaitInSeconds";

        /// <inheritdoc/>
        [TimeAndLogAspect]
        public override void Execute(bool log, string name, bool performAction, bool runAODA, string runAODAPageName, XmlNode testActionInformation, SeleniumDriver seleniumDriver, CSVLogger csvLogger)
        {
            if (!performAction)
            {
                return;
            }

            int seconds = Convert.ToInt32(testActionInformation.Attributes["seconds"].Value);
            Thread.Sleep(seconds);
        }
    }
}
