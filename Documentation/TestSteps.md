# Test Steps

This is the fifth element inside `<TestSet>`. Inside this element, you specify a test action that you would run for this test step. Here, each action is its own element, but they all share a common set of attribute values, and some require specific attributes.

## Common Attributes
Attribute Name | Mandatory \ Optional | Description | Example
:------------- | :------------------: | :---------- | :------
id             | `M`                  | Identifier for this test step. Used to relate `<TestCases>` and `<TestSet>` child elements. | `id="Signing into Facebook"`
name           | `M`                  | Name used for logging this test step. | `name="Siging into Facebook with my account"`
runAODA        | `O`                  | Used to tell the framework whether to run accessiblity checking or not. Values can be either `true` or `false`. If set to `true`, `runAODAPAgeName` must be provided. Default value is set to `false`. For more accessibility results information, refer to [Accessibility](/Documentation/Accessibility.md). |  `runAODA="true"`
runAODAPageName | `M`                 | Used for logging results when running accessibility checks. For more accessibility results, refer to [Accessibility](/Documentation/Accessibility.md). | `runAODAPageName="Facebook Home Page"`
log             | `O`                 | Used to tell whether to log this test step or not. By default, the value is `true`. Values can be either `true` or `false`. | `log="false"`

## Test Actions
Test Action Name | Description | Attributes | Example
:--------------- | :---------- | :--------- | :-----
ChooseCollection | Chooses a collection | `collectionSearchField` is for the collection drop down list search bar. `collectionName` is for the actual collection name. | `<ChooseCollection id='Fake Test Step ID 1' name='Fake Step Name 1' collectionSearchField="Search Text" collectionName="Collection Name"/>` 
ClickElementByXPath | Clicks an element found using the specified xpath. | `xPath` is the xpath to find the web element. | `<ClickElementByXPath id='Fake Test Step ID 2' name='Fake Step Name 2' xPath="Fake xPath"/>`
CloseBrowser  | Closes the browser | | `<CloseBrowser id='Fake Test Step ID 3' name='Fake Step Name 3'/>`
DDLSelectByXPath | Selects a value in a drop down list found by the specified xpath. | `xPath` is the xpath to find the drop down list. `selection` is the value to be selected. | `<DDLSelectByXPath id='Fake Test Step ID 5' name='Fake Step Name 5' xPath="Fake xPath" selection="value to be selected"/>`
MaximizeBrowser  | Maximizes the browser | | `<MaximizeBrowser id='Fake Test Step ID 3' name='Fake Step Name 3'/>`
OpenBrowser      | Opens the browser with the provided url / environment url. | | `<OpenBrowser id='Fake Test Step ID 6' name='Fake Step Name 6'/>`
PopulateElementByXPath | Populates an element found by the specified xPath with the provided text | `xPath` is the xpath to find the web element. `text` is the value to poulate into the element. Optional attribute `useJS` can be toggled to `true` or `false`. Default value is false. `useJS` should be the last resort.| `<PopulateElementByXPath id='Fake Test Step ID 7' name='Fake Step Name 7' xPath="Fake xPath" text="value"/>`
SignIn                 | Signs into the login page. | `username` is the username to use to login. `password` is the password to use to login. | `<SignIn id='Fake Test Step ID 8' name='Fake Step Name 8' username="fake username" password="fake password"/>`
SwitchIntoIFrame       | Switches the context into an IFRAME | `xPath` is the xpath to find the IFRAME. Must switch into an IFRAME if the element belongs inside an IFRAME. Set `xPath` to __root__ if you want to switch context back to the root HTML document. | `<SwitchIntoIFrame id='Fake Test Step ID 9' name='Fake Step Name 9' xPath="Fake xPath"/>`
SwitchToTab            | Switches the focus to the specified tab. Index of tabs start from 1. | `tabIndex` is the index of the tab you want to focus. | `<SwitchToTab id='Fake Test Step ID 10' name='Fake Step Name 10' tabIndex="1"/>`
WaitForElement | Waits until the element is visible / invisible. | `xPath` is the xpath to find the web element. Setting `invisible` to `true` means waiting until the element is invisible, and `false` means until it is visible. | `<WaitForElement id='Fake Test Step ID 11' name='Fake Step Name 11' xPath="Fake xPath" invisible="false"/>`
WaitInSeconds | Waits for x amount of seconds | `seconds` is the interger amount of seconds to wait | `<WaitInSeconds id='Fake Test Step ID 12' name='Fake Step Name 12' seconds="10"/>` 
