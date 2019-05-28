# DataFile

We can specify a data file when we want to tokenize values in the XML file.

To specify a token, we wrap it surround the token name with "${{" and "}}".

Example:

The xml file we use:
```xml
<SpecialElements>
    <!-- explicitly give the xpath for the loading spinner if any -->
    <LoadingSpinner>${{MY LOADING SPINNER}}</LoadingSpinner>
</SpecialElements>
```

The data file we provide the file path to should look like:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Tokens>
	<Token key="MY LOADING SPINNER" value="//*[@id='loadingspinner']"/>
</Tokens>
```

Any value that is not restricted to be integers / boolean can be tokenized.
You can token part of the value.

The xml file we use:
```xml
<SpecialElements>
    <!-- explicitly give the xpath for the loading spinner if any -->
    <LoadingSpinner>//*[@id='${{LoadingSpinnerID}}']</LoadingSpinner>
</SpecialElements>
```

The data file we provide the file path to should look like:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Tokens>
	<Token key="LoadingSpinnerID" value="loadingspinner"/>
</Tokens>
```

This may be useful for test cases pertaining to signing in, as the username and password may change depending on the environment used.