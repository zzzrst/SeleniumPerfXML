using SeleniumPerfXML.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumPerfXMLNUnitTest
{
    class FakeTestStep : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "FakeTestStep";
    }
}
