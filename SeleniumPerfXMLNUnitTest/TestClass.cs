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

            TestSetBuilder builder = new TestSetBuilder("C:\\SeleniumPerfXML\\Testing\\TestBareMinimum.xml")
            {
                URL = "testurl",
                CsvSaveFileLocation = "C:\\SeleniumPerfXML\\Testing\\Files",
                LogSaveFileLocation = "C:\\SeleniumPerfXML\\Testing\\Files",
                ScreenshotSaveLocation = "C:\\SeleniumPerfXML\\Testing\\Files",
                ReportSaveFileLocation = "C:\\SeleniumPerfXML\\Testing\\Files",
                XMLFile = "C:\\SeleniumPerfXML\\Testing\\TestBareMinimum.xml",
            };

            testStep = builder.BuildTestSet();
            AutomationTestSetDriver.RunTestSet(testStep);
            testStep.Reporter.Report();
            Assert.Pass();
        }
    }
}
