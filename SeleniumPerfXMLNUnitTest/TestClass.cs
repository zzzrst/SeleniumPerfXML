using System;
using AutomationTestSetFramework;
using CommandLine;
using NUnit.Framework;
using SeleniumPerfXML;
using SeleniumPerfXML.Implementations;
using System.Configuration;

namespace SeleniumPerfXMLNUnitTest
{
    public class TestClass
    {
        private string[] args;

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void TestBareMinimum()
        {
            var keys = ConfigurationManager.AppSettings.AllKeys; //["LoadingSpinner"].ToString();
            TestSetXml testStep;

            TestSetBuilder builder = new TestSetBuilder("C:\\SeleniumPerfXML\\TestBareMinimum.xml")
            {
                URL = "testurl",
                CsvSaveFileLocation = "C:\\SeleniumPerfXML\\Files",
                LogSaveFileLocation = "C:\\SeleniumPerfXML\\Files",
                ScreenshotSaveLocation = "C:\\SeleniumPerfXML\\Files",
                ReportSaveFileLocation = "C:\\SeleniumPerfXML\\Files",
                XMLFile = "C:\\SeleniumPerfXML\\TestBareMinimum.xml",
            };

            testStep = builder.BuildTestSet();
            AutomationTestSetDriver.RunTestSet(testStep);
            testStep.Reporter.Report();
            Assert.Pass();
        }
    }
}
