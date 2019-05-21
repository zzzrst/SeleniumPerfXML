# Hello World.
Here, we will walkthrough a simple example of navigating to www.google.ca using Chrome.

## XML:
```XML
<?xml version="1.0" encoding="utf-8" ?>
<TestSet xmlns="http://qa/SeleniumPerf">
	<Parameters>
		<Browser>chrome</Browser>
		<Environment>Hello World!</Environment>
		<URL>www.google.ca</URL>
				
		<WarningThreshold>60</WarningThreshold>
		<TimeOutThreshold>120</TimeOutThreshold>
		
		<CSVSaveLocation>C:\Selenium\CSV\</CSVSaveLocation>
	</Parameters>
	
	<SpecialElements/>
	
	<TestCaseFlow>
		<RunTestCase>Navigate to Google</RunTestCase>
	</TestCaseFlow>
	
	<TestCases>
		<TestCase name='Hello World - Navigate to Google!' id='Navigate to Google'>
			<RunTestStep>Opening Browser</RunTestStep>
		</TestCase>
	</TestCases>
	
	<TestSteps>
		<OpenBrowser id='Opening Browser' name='Hello World - Opening Browser'/>					 
	</TestSteps>

</TestSet>

```

## Running: 
To run, you would call on the following:
```PowerShell
> dotnet {Selenium Perf XML Location}\SeleniumPerfXML.ddl --XMLFile "{XML File Location}"
```

Example:
```PowerShell
> dotnet .\SeleniumPerfXML.ddl --XMLFile ".\HelloWorld.xml"
```


## Explanation: 
In this `HelloWorld.xml`, we have parameters, special elements, test case flow, test cases, and test steps inside the root element test set. 

Parameters are used for the overall settings for this test set. The required parameters are browser, environment, warning threshold, time-out threshold and CSV save location. For more information about parameters, refer to [here](/Documentation/Parameters.md). 

Special elements are used to define special elements the framework has built in logic for. For more information about special elements, refer to [here](/Documentation/SpecialElements.md).

Test case flow is used to define the execution flow of the testcases. While in this example, there is only one test case, you can create complicated execution flows when including if conditions. Inside `<RunTestCase>` element, the user references the test case by its ID. For more information, refer to [here](/Documentation/TestCaseFlow.md).

Test cases are used to define the different test cases. Like above, you can use if conditions to create a complicated execution flow for its test steps. Inside `<RunTestStep>` element, the user references the test step by its ID. For more information, refer to [here](/Documentation/TestCases.md).

Test steps are the smallest unit of work defined in this xml. It usually represents one action, or set of actions logically making a step. There are many different types of test step. For each test step, you must include an ID and name. For every other test step, it may require more attributes. For more information, refer to [here](/Documentation/TestSteps.md).

