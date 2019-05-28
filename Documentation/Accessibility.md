# Accessibility

Here we run accessibility using the axe-min.js file found from Axe. 
We re-organize the results from the js to be in the format of

1.  TalliedResult.csv
    * This includes the tallied results for each page.
    * Format: __Page URL, Provided Page Title, Browser Page Title, Passes, Violations, Incomplete, Inapplicable__

2.  RulePageSummary.csv
    * This includes the tallied results for each rule on each page.
    * Format: __Page URl, Provided Page Title, Browser Page Title, Result Type, Description, Rule Tag, Impact, Help, Help URL, Occurance on Page__

3.  Json\\{ruleTag}\_{ruleID}\_{resultType}.json
    * The folder Json will include json files that contains all results for each rule and result type.
    * A file for rule A and result type of violation would contain all the violated occurances for all pages that this script was run on.
    * Example:
    
        ![AODA_JSON_RESULTS](/Documentation/Accessibility/AODA_JSON_RESULTS.png)
