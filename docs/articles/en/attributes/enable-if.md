# Enable If Attribute

The field becomes editable only if the boolean value of the target member is true.

![img](../../../images/img-attribute-enable-if-false.png)

![img](../../../images/img-attribute-enable-if-true.png)

```cs
public bool isEnabled;

public bool IsEnabled => isEnabled;
public bool IsEnabledMethod() => isEnabled;

[EnableIf("isEnabled")]
public int enableIfField;

[EnableIf("IsEnabled")]
public int enableIfProperty;

[EnableIf("IsEnabledMethod")]
public int enableIfMethod;
```

| Parameter | Description |
| - | - |
| Condition | The name of the field, property, or method used for condition evaluation |
