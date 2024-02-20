# Hide If Attribute

Hides the member in the Inspector if the boolean value is true.

![img](../../../images/img-attribute-hide-if-false.png)

![img](../../../images/img-attribute-hide-if-true.png)

```cs
public bool hide;

public bool Hide => hide;
public bool IsHideTrue() => hide;

[HideIf("hide")]
public int hideIfField;

[HideIf("Hide")]
public int hideIfProperty;

[HideIf("IsHideTrue")]
public int hideIfMethod;
```

| Parameter | Description |
| - | - |
| Condition | The name of the field, property, or method used for the condition evaluation. |