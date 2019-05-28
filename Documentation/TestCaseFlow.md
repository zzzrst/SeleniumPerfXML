# Test Case Flow

This is the third element inside `<TestSet>`. Inside this element, you specify the execution flow for the test cases. In the most simple case, you would include the child element `<RunTestCase>` with its inner text the `id` of a TestCase. For information about `<TestCases>`, please refer to [TestCases](\Documentation\TestCases.md).

## Example 1 - Simplist Test Case Flow

```xml
<TestCaseFlow>
    <RunTestCase>Navigate to Google</RunTestCase>
</TestCaseFlow>

<TestCases>
    <TestCase name='Hello World - Navigate to Google!' id='Navigate to Google'>
        <RunTestStep>Opening Browser</RunTestStep>
    </TestCase>
</TestCases>
```

If we are given this XML provided above, our execution of test cases include only one test case __"Navigate to Google"__. Here, the framework will run the test case with the `id` of __"Navigate to Google"__.

## Example 2 - Duplicate IDs - What happens?

```xml
<TestCaseFlow>
    <RunTestCase>Navigate to Google</RunTestCase>
</TestCaseFlow>

<TestCases>
    <TestCase name='Hello World - Navigate to Google!' id='Navigate to Google'>
        <RunTestStep>Opening Browser</RunTestStep>
    </TestCase>
    <TestCase name='Hello World - Open Browser!' id='Navigate to Google'>
        <RunTestStep>Close Browser</RunTestStep>
    </TestCase>
</TestCases>
```
If we are given this XML provided above, our execution of test cases include only one test case __"Navigate to Google"__. Here, the framework will run the first test case with the `id` of __"Navigate to Google"__. Therefore, we would be looking at this element for the test case:

```xml
    <TestCase name='Hello World - Navigate to Google!' id='Navigate to Google'>
        <RunTestStep>Opening Browser</RunTestStep>
    </TestCase>
```
We also note, that the `name` of the `TestCase` element can be different than its `id`.

## Example 3 - Conditions
For any automation, we usually include some type of conditional logic. Inside a test case execution flow, you can introduce the `<IF>` child element to check for an web element's condition. A web element condition can be either `"Exist"` or `"DNE`". 

### Example 3.1 Simple If Condition

```xml
<TestCaseFlow>
    <If elementXPath='//div' condition='EXIST'>
        <Then>
            <RunTestCase>Fake Test Case ID 2</RunTestCase>
            <RunTestCase>Fake Test Case ID 3</RunTestCase>
        </Then>
    </If>
</TestCaseFlow>
```

If we are given the XML above, the framework would interpret it as checking whether an web element with `xpath` of __"//div"__ `"EXIST"`. If it does, __then__ it will run the block inside the child element `<THEN>`. Here, it will run both __"Fake Test Case ID 2"__ and __"Fake Test Case ID 3"__.

Note that this definition is recursive, so the following is also a valid.

```xml
<TestCaseFlow>
    <If elementXPath='//div' condition='EXIST'>
        <Then>
            <If elementXPath='//a' condition='EXIST'>
                <Then>
                    <RunTestCase>Fake Test Case ID 2</RunTestCase>
                </Then>
            </If>
            <RunTestCase>Fake Test Case ID 3</RunTestCase>
        </Then>
    </If>
</TestCaseFlow>
```


### Example 3.2 Conditions with ElseIf and Else

Like any programming language, we can specify _else if_ and _else_ logic.

```xml
<TestCaseFlow>
    <If elementXPath='//div' condition='EXIST'>
        <Then>
            <RunTestCase>Fake Test Case ID 1</RunTestCase>
        </Then>
        <ElseIf elementXPath='//a' condition='DNE'>
            <RunTestCase>Fake Test Case ID 2</RunTestCase>
        </ElseIf>
        <Else>
            <RunTestCase>Fake Test Case ID 3</RunTestCase>
            <If elementXPath='//img' condition='EXIST'>
                <Then>
                    <RunTestCase>Fake Test Case ID 4</RunTestCase>
                </Then>
                <Else>
                    <RunTestCase>Fake Test Case ID 5</RunTestCase>
                </Else>
            </If>
        </Else>
    </If>
    <RunTestCase>Fake Test Case ID 6</RunTestCase>
</TestCaseFlow>
```

The framework would interpret this test case execution flow as:
![Execution Flow Chart](/Documentation/TestCaseFlow/TestCaseFlowDiagram.png)
