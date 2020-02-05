using SeleniumPerfXML.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumPerfXMLNUnitTest
{
    class FakeFailingTestStep : TestStepXml
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "FakeFailingTestStep";

        /// <inheritdoc/>
        public override void Execute()
        {
            base.Execute();
            throw new Exception("Fake Test Step Failed!");
        }
    }
}
