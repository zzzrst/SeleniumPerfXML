namespace SeleniumPerfXML.Implementations
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
        public int TotalTestCases
        {
            get => this.TestCases.Count;
            set => this.TotalTestCases = this.TestCases.Count;
        }

        /// <inheritdoc/>
        public ITestSetStatus TestSetStatus { get; set; }

        /// <inheritdoc/>
        public int CurrTestCaseNumber { get; set; }

        /// <inheritdoc/>
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        /// <summary>
        /// Gets or sets list of testcases to run.
        /// </summary>
        public List<TestCaseXml> TestCases { get; set; }

        /// <inheritdoc/>
        public bool ExistNextTestCase()
        {
            return this.CurrTestCaseNumber + 1 < this.TotalTestCases;
        }

        /// <inheritdoc/>
        public ITestCase GetNextTestCase()
        {
            this.CurrTestCaseNumber += 1;
            return this.TestCases[this.CurrTestCaseNumber];
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
