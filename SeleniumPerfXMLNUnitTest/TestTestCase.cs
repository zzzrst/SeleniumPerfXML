using System;
using AutomationTestSetFramework;
using CommandLine;
using NUnit.Framework;
using SeleniumPerfXML;
using SeleniumPerfXML.Implementations;
using System.Configuration;
using System.IO;
using SeleniumPerfXML.Implementations.Loggers_and_Reporters;

namespace SeleniumPerfXMLNUnitTest
{
    public class TestTestCase
    {
        public string saveFileLocation;
        public string readFileLocation;
        public string logName;
        public string reportName; 

        [SetUp]
        public void SetUp()
        {
            saveFileLocation = "C:\\SeleniumPerfXML\\Testing\\Files";
            readFileLocation = "C:\\SeleniumPerfXML\\Testing\\TestTestCase";
            logName = "\\Log.txt";
            reportName = "\\Report.txt";
            // Removes all previous ran test results
            // If directory does not exist, don't even try   
            if (Directory.Exists(saveFileLocation))
            {
                if (File.Exists(saveFileLocation + logName))
                    File.Delete(saveFileLocation + logName);
                if (File.Exists(saveFileLocation + reportName))
                    File.Delete(saveFileLocation + reportName);
                Directory.Delete(saveFileLocation);
            }
        }

        [Test]
        public void TestDuplicateIDs() { }

        [Test]
        public void TestSimpleIf() { }

        [Test]
        public void TestIfChain() { }

        [Test]
        public void TestElseIf() { }

        [Test]
        public void TestElse() { }
    }
}
