# Welcome!

### This repository contains the source code for:
- Automated Tests written in XML to be executed by Selenium (SeleniumPerfXML)

# Overview
Please take a few minutes to review the overview below before diving into the code.

## Selenium.Axe
Selenium.Axe is the integration of axe-core and Selenium. The github source for the nugget package can be found [here](https://github.com/TroyWalshProf/SeleniumAxeDotnet).

Dependencies:

 Name | Version 
:---- | :-------
.NetCore.App | 2.1

## SeleniumPerfXML
SeleniumPerfXML is an automation framework that reads an XML file for instructions that will be performed on the browser.

Dependencies:

 Name | Version 
:---- | :-------
.NetCore.App | 2.1


# Getting Started
* You must have .Net Core 2.1 SDK / Runtime installed.
* To use chrome as a browser, you must: 
    * Download version 73 of chrome/chromium
    * Rename the installation folder as chromium
    * Copy that folder into the same location as the built solution

## Documentation
Refer to the following documentation to get started:

*  [Hello World!](/Documentation/HelloWorld.md) is an example to quickly get started.
*  [Parameters](/Documentation/Parameters.md) goes into depth the different parameters inside the XML file and command line arguments. 
*  [Special Elements](/Documentation/SpecialElements.md) goes into depth the different special elements the framework has logic for.
*  [Test Case Flow](/Documentation/TestCaseFlow.md) goes into depth of how to set up the test case flow.
*  [Test Cases](/Documentation/TestCases.md) goes into depth of how to create test cases.
*  [Test Steps](/Documentation/TestSteps.md) goes into depth of the different type of test steps.
*  [CSV Logging](/Documentation/CSVLogging.md) goes into depth the logic behind the csv logging.
*  [Accessibility](/Documentation/Accessibility.md) goes into depth how to use the framework to test accessibility.
*  [DataFile](/Documentation/DataFile.md) goes into depth how to use a seperate data file with XML
*  [Logging](/Documentation/Logging.md) goes into depth how internal logging for the framework is setup.
*  [Sample Test Data XML File](/SeleniumPerfXML/SampleTestData.xml) is the sample xml file with many different combinations to show case the possibilities.
*  Also, you can validate your XML with the following [XSD](/SeleniumPerfXML/SeleniumPerf.xsd).
