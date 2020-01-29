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
        public string Name { get; set; }
        public int TotalTestCases { get; set; }

        public ITestSetStatus TestSetStatus => throw new NotImplementedException();

        public int CurrTestCaseNumber { get; set; }
        public IMethodBoundaryAspect.FlowBehavior OnExceptionFlowBehavior { get; set; }

        public bool ExistNextTestCase()
        {
            throw new NotImplementedException();
        }

        public ITestCase GetNextTestCase()
        {
            throw new NotImplementedException();
        }

        public void HandleException(Exception e)
        {
            throw new NotImplementedException();
        }

        public void SetUp()
        {
            throw new NotImplementedException();
        }

        public bool ShouldExecute()
        {
            throw new NotImplementedException();
        }

        public void TearDown()
        {
            throw new NotImplementedException();
        }

        public void UpdateTestSetStatus(ITestCaseStatus testCaseStatus)
        {
            throw new NotImplementedException();
        }
    }
}
