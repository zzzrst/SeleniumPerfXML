namespace SeleniumPerfXML.TestSteps
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutomationTestSetFramework;

    /// <summary>
    /// Implementation of the ITestSet Class.
    /// </summary>
    public class TestSetXml : ITestSet
    {
        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public int TotalTestCases { get; set; }

        /// <inheritdoc/>
        public ITestSetStatus TestSetStatus { get; set; }

        /// <inheritdoc/>
        public int CurrTestCaseNumber { get; set; }

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// List of testcases to run.
        /// </summary>
        public List<TestCase> TestCases { get; set; }

        /// <inheritdoc/>
        public bool ExistNextTestCase()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ITestCase GetNextTestCase()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void HandleException(Exception e)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void SetUp()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public bool ShouldExecute()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void TearDown()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void UpdateTestSetStatus(ITestCaseStatus testCaseStatus)
        {
            throw new NotImplementedException();
        }
    }
}
