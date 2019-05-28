# Special Elements

The framework has built in logic for the following elements.

## 1. Loading Spinner
This is an optional element to be added in the xml. To add you would provide the following:

```xml
<SpecialElements>
    <!-- explicitly give the xpath for the loading spinner if any -->
    <LoadingSpinner>//*[@id='loadingspinner']</LoadingSpinner>
</SpecialElements>
```

Note that you would have to provide the xpath for the loading spinner. This spinner is checked for at the end of each test step and will be included in the timing of the transaction. It will try to wait until the spinner is invisible / not present.


## 2. Error Container
This is an optional element to be added in the xml. To add, you would provide the following:

```xml
<SpecialElements>
    <!-- will check this xpath and log anything found in this element if found -->
    <ErrorContainer>//div[@class='alert alert-danger']</ErrorContainer>
</SpecialElements>
```

Note that you would have to provide the xpath for the container for any errors the application / website would show on the UI. This is useful if the application has one common place that throws errors. This will check if this container element is present after the loading spinner is invisible / not present. If present, it will be logged inside the SeleniumPerfXML.log file with log level of `ERROR`. This would be useful when debugging why an transaction has an `F`.
