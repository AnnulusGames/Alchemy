# Disable If Attribute

If the boolean value of the target member is true, the field becomes disabled.

![img](../../../images/img-attribute-disable-if-false.png)

![img](../../../images/img-attribute-disable-if-true.png)

```cs
public bool isDisabled;

public bool IsDisabled => isDisabled;
public bool IsDisabledMethod() => isDisabled;

[DisableIf("isDisabled")]
public int disableIfField;

[DisableIf("IsDisabled")]
public int disableIfProperty;

[DisableIf("IsDisabledMethod")]
public int disableIfMethod;
```

| Parameter | Description |
| - | - |
| Condition | The name of the field, property, or method used for condition evaluation. |