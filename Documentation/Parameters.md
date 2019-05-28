# Parameters

Parameters refer to the settings pertaining to the whole test set.

## Outline
1. [Parameter Description](/Documentation/Parameters.md#parameter-description)
2. [Example 1 - The Bare Minumum:](/Documentation/Parameters.md#example-1---the-bare-minumum)
3. [Example 2 - Introducing Sanity to Files Saved](/Documentation/Parameters.md#example-2---introducing-sanity-to-files-saved)
4. [Example 3 - Tokenizing Values](/Documentation/Parameters.md#example-3---tokenizing-values)
5. [Example 4 - Setting Flags for respectRepeatFor & respectRunAODAFlag](/Documentation/Parameters.md#example-4---setting-flags-for-respectrepeatfor--respectrunaodaflag)
6. [Overriding Parameters from the Command Line](/Documentation/Parameters.md#overriding-parameters-from-the-command-line)
9. [Example 5 - Use case for running AODA test on demand.](/Documentation/Parameters.md#example-5---use-case-for-running-aoda-test-on-demand)

### Parameter Description
Name | Mandatory \ Optional | Description
:--- | :------------------: | :------------
Browser | `M`               | This chooses the browser to use for this test set. Please use values in {"chrome", "ie", "firefox", "edge"} as the value.
Environment | `M`           | This specifies the environment the test set will be using. If the environment is a key-value pair inside `<appSettings>` of  [`App.config`](/SeleniumPerfXML/App.config) for SeleniumPerfXML, it will use the valueas the url.
URL | `O` | If provided, the url used to launch the browser will be the given value instead of the one found for Environment.
RespectRepeatFor | `O` | The value provided must be either 'true' or 'false'. If provided value is 'true', it will respect the `repeatFor` value specified for each test case. If provided value is 'false', all test cases will not respect the `repeatFor` value, and run once. For more info, refer to [TestCases](/Documentation/TestCases.md). 
RespectAODAFlag | `O` | The value provided must be either 'true' or 'false'. If provided value is 'true', it will respect the `runAODA` flag set for the test step. If the provided value is `false`, it will not run AODA after the test step. For test step configuration, refer to [TestSteps](/Documentation/TestSteps.md). For more information on web accessbility, refer to [Accessbility](/Documentation/Accessibility.md).
WarningThreshold | `M` | The value provided must be smaller than the timeout threshold. This value is to provide a threshold for the transaction to be a "warning".
TimeOutThreshold | `M` | The value provided is the timeout value set for a transaction to be performed. If the transaction could not be performed within this time, it would be recorded as an "F".
DataFile | `O` | The value provided is the file path to a data file. The data file is used for tokenizing values inside the XML file, and having environment specific values in their own data files. For more info, please refer to [DataFile](/Documentation/DataFile.md).
CSVSaveLocation | `M` | The value provided is the folder path to save CSV file for transaction logging. The name of the CSV file will be the same as the XML file name. For information about CSV logging, refere to [CSVLogging](/Documentation/CSVLogging.md).
LogSaveLocation | `O` | The value provided is the folder path to save AODA result files. If not provided, it will use the same folder path as CSVSaveLocation. For more information on the result files, refer to [Accessibility](/Documentation/Accessibility.md).
ScreenshotSaveLocation | `O` |  The value provided is the folder path to save screenshots of the web browser when the transaction has failed.


### Example 1 - The Bare Minumum:

```xml
<Parameters>
    <Browser>chrome</Browser>
    <Environment>EDCS-9</Environment>
    
    <WarningThreshold>60</WarningThreshold>
    <TimeOutThreshold>120</TimeOutThreshold>

    <CSVSaveLocation>C:\Selenium\CSV\</CSVSaveLocation>
</Parameters>
```

### Example 2 - Introducing Sanity to Files Saved

```xml
<Parameters>
    <Browser>chrome</Browser>
    <Environment>EDCS-9</Environment>

    <WarningThreshold>60</WarningThreshold>
    <TimeOutThreshold>120</TimeOutThreshold>

    <CSVSaveLocation>C:\Selenium\CSV\</CSVSaveLocation>
    <LogSaveLocation>C:\Selenium\Logs\</LogSaveLocation>
    <ScreenshotSaveLocation>C:\Selenium\Screenshots\</ScreenshotSaveLocation>
</Parameters>
```

### Example 3 - Tokenizing Values

```xml
<Parameters>
    <Browser>chrome</Browser>
    <Environment>EDCS-9</Environment>

    <WarningThreshold>60</WarningThreshold>
    <TimeOutThreshold>120</TimeOutThreshold>

    <DataFile>sampleDataFile</DataFile>
    <CSVSaveLocation>C:\Selenium\CSV\</CSVSaveLocation>
</Parameters>
```
### Example 4 - Setting Flags for respectRepeatFor & respectRunAODAFlag

```xml
<Parameters>
    <Browser>chrome</Browser>
    <Environment>EDCS-9</Environment>
    
    <RespectRepeatFor>true</RespectRepeatFor>
    <RespectRunAODAFlag>false</RespectRunAODAFlag>

    <WarningThreshold>60</WarningThreshold>
    <TimeOutThreshold>120</TimeOutThreshold>

    <CSVSaveLocation>C:\Selenium\CSV\</CSVSaveLocation>
</Parameters>
```

### Overriding Parameters from the Command Line
All the parameters inside the XML file can be overriden. This is to allow control on the XML file level, and the command line level. If parameter value is not overriden, it will use values specified in the XML file.

You would append this after calling on this application.
One way to call this application would be: `dotnet "{fileLocation}\SeleniumPerfXML.dll" --XMLFile "{XML File Location}"`

Parameter | Command Line Override Name | Example
:-------- | :------------------------- | :------
Browser | `b` or `browser`| `--b chrome` or `--browser ie`
Environment | `e` or `environment` | `--e EDCS-9` or `--environment EDCS-9`
URL | `u` or `url` | `--u www.google.ca` or `--url www.google.ca`
RespectRepeatFor | `respectRepeatFor` | `--respectRepeatFor true`
RespectAODAFlag | `respectRunAODAFlag` | `--respectRunAODAFlag true`
WarningThreshold | `warningThreshold` | `--warningThreshold 12`
TimeOutThreshold | `timeOutThreshold` | `--timeOutThreshold 50`
DataFile | `dataFile` | `--dataFile "{File Location}"`
CSVSaveLocation | `csvSaveFileLocation` | `--csvSaveFileLocation "{Folder Location}"`
LogSaveLocation | `logSaveLocation` | `--logSaveLocation "{Folder Location}"`
ScreenshotSaveLocation | `screenShotSaveLocation` | `--screenShotSaveLocation "{Folder Location}"`

### Example 5 - Use case for running AODA test on demand.
Here we will show the XML and the overridden parameters to run AODA test on demand, but allow the performance measurement be affected.

XML:
```xml
<Parameters>
    <Browser>chrome</Browser>
    <Environment>EDCS-9</Environment>

    <RespectRepeatFor>true</RespectRepeatFor>
    <RespectRunAODAFlag>false</RespectRunAODAFlag>

    <WarningThreshold>60</WarningThreshold>
    <TimeOutThreshold>120</TimeOutThreshold>

    <DataFile>sampleDataFile</DataFile>
    <CSVSaveLocation>C:\Selenium\CSV\</CSVSaveLocation>
    <LogSaveLocation>C:\Selenium\Logs\</LogSaveLocation>
    <ScreenshotSaveLocation>C:\Selenium\Screenshots\</ScreenshotSaveLocation>
</Parameters>
```

Command Line:
```console
foo@bar:~$ dotnet "{fileLocation}\SeleniumPerfXML.dll" --XMLFile "{XML File Location}" --respectRepeatFor false --respectRunAODAFlag true
```

Explanation:

Here the XML specifies to respect the repeat for flag set for each test case, and also not to respect the runAODA flag set fo each test step. This means each test case can run as many times as specified for performance measurement. We will not run AODA after the test step regardless if specified to run / not. However, when we need to run AODA on demand, we can use the same XML file, but override it from command line with the values `--respectRepeatFor false --respectRunAODAFlag true` such that all test cases will only run once, and we runAODA after the test step based on the setup in the XML file. For information on setting up runAODA, please refer to [TestSteps](/Documentation/TestSteps.md). For information on the results for AODA, please refer to [Accessibility](/Documentation/Accessibility.md).