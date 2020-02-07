// <copyright file="FakeTestStep.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXMLNUnitTest
{
    using SeleniumPerfXML.Implementations;

    /// <summary>
    /// This test step is the same as testStepXml.
    /// </summary>
    public class FakeTestStep : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "FakeTestStep";
    }
}
