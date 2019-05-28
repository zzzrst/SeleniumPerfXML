# CSV Logging

The framework will log each test step it goes through.

## Logic of the logging
Perform | Log | Behaviors
:-----: | :-: | :---
Y       | Y   | `Transaction Name, Time in Seconds` or `Transaction Name, "F"`
Y       | N   | No entry
N       | Y   | `Transaction Name, "N/A"`
N       | N   | No entry

\* Transaction Name is the value of the attribute `name` for this test step.

\*\* A test step will not be performed when the test case execution flow / test step execution flow _skips_ this test case because of failing / passing a condition.

## Possible Logging Values
Value | Meaning
:-----| :------
`F`   | The test step failed to be performed within the timeout threshold.
`N\A` | The test step was not performed because of conditions in execution flow.
`Time in Seconds` | The test step took this amount of seconds from start to finish.

## Sample CSV Logging 

This would be the result.

    Transaction, 05/24/2019 6:56:53 PM
    Environment URL, www.google.ca
    Hello World - Opening Browser, "0.1532233"

If we have multiple runs, it would extend as follows:

    Transaction, 05/24/2019 6:56:53 PM, 05/25/2019 6:36:45 PM
    Environment URL, www.google.ca, www.google.ca
    Hello World - Opening Browser, "0.1532233", "0.1478666"