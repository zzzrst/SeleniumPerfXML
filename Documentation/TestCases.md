# TestCases

This is the fourth element inside `<TestSet>`. Inside this element, you specify the execution flow of test steps within this test case. In the simplist case, you would include the child element `<RunTestStep>` with its inner text the `id` of a `<TestStep>`. For information about `<TestStep>`, please refer to [TestSteps](\Documentation\TestSteps.md).

## Simplist Test Case

```xml
<TestCases>
    <TestCase name='Hello World - Navigate to Google!' id='Navigate to Google'>
        <RunTestStep>Opening Browser</RunTestStep>
    </TestCase>
</TestCases>

<TestSteps>
    <OpenBrowser id='Opening Browser' name='Hello World - Opening Browser'/>
</TestSteps>
```

If we are given this XML provided above, our execution of the test case __"Navigate to Google"__ includes only of the test step __"Opening Browser"__. 

## Duplicate IDs - What happens?

```xml
<TestCases>
    <TestCase name='Hello World - Navigate to Google!' id='Navigate to Google'>
        <RunTestStep>Opening Browser</RunTestStep>
    </TestCase>
</TestCases>

<TestSteps>
    <OpenBrowser id='Opening Browser' name='Hello World - Opening Browser'/>
    <OpenBrowser id='Opening Browser' name='Hello World - Opening thy Browser'/>
</TestSteps>
```
If we are given this XML provided above, our execution of the test case include only one test step __"Opening Browser"__. Here, the framework will run the first test step with the `id` of __"Opening Browser"__. Therefore, we would be looking at this element for the test step:

```xml
<TestSteps>
    <OpenBrowser id='Opening Browser' name='Hello World - Opening Browser'/>
</TestSteps>
```
We also note, that the `name` of the child elements in `TestSteps` element can be different than its `id`.

## Conditions
For any automation, we usually include some type of conditional logic. Inside a test case execution flow, you can introduce the `<IF>` child element to check for an web element's condition. A web element condition can be either `"Exist"` or `"DNE`". 

### Simple If Condition

```xml
<TestCases>
    <If elementXPath='//div' condition='EXIST'>
        <Then>
            <RunTestStep>Fake Test Step ID 2</RunTestStep>
            <RunTestStep>Fake Test Step ID 3</RunTestStep>
        </Then>
    </If>
</TestCases>
```

If we are given the XML above, the framework would interpret it as checking whether an web element with `xpath` of __"//div"__ `"EXIST"`. If it does, __then__ it will run the block inside the child element `<Then>`. Here, it will run both __"Fake Test Step ID 2"__ and __"Fake Test Step ID 3"__.

Note that this definition is recursive, so the following is also a valid.

```xml
<TestCases>
    <If elementXPath='//div' condition='EXIST'>
        <Then>
            <If elementXPath='//a' condition='EXIST'>
                <Then>
                    <RunTestStep>Fake Test Step ID 2</RunTestStep>
                </Then>
            </If>
            <RunTestStep>Fake Test Step ID 3</RunTestStep>
        </Then>
    </If>
</TestCases>
```


### Conditions with ElseIf and Else

Like any programming language, we can specify _else if_ and _else_ logic.

```xml
<TestCases>
    <If elementXPath='//div' condition='EXIST'>
        <Then>
            <RunTestStep>Fake Test Step ID 1</RunTestStep>
        </Then>
        <ElseIf elementXPath='//a' condition='DNE'>
            <RunTestStep>Fake Test Step ID 2</RunTestStep>
        </ElseIf>
        <Else>
            <RunTestStep>Fake Test Step ID 3</RunTestStep>
            <If elementXPath='//img' condition='EXIST'>
                <Then>
                    <RunTestStep>Fake Test Step ID 4</RunTestStep>
                </Then>
                <Else>
                    <RunTestStep>Fake Test Step ID 5</RunTestStep>
                </Else>
            </If>
        </Else>
    </If>
    <RunTestStep>Fake Test Step ID 6</RunTestStep>
</TestCases>
```

The framework would interpret the test step execution flow as:
![Execution Flow Chart](/Documentation/TestCases/TestCasesDiagram.png)

## Attributes
Attribute name | Description
:------------- | :----------
id             | The identifier for this test case. Used to relate `<TestCaseFlow>` and `<TestCase>`
name           | The name for this test case. Used for logging.
repeatFor      | Optional attribute. Used to determine how many times to run this test case. Value can be any interger above and include zero. Default value is 1. 