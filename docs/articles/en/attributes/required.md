# Required Attribute

Displays a warning if the field does not have an object reference assigned.

![img](../../../images/img-attribute-required.png)

```cs 
[Required]
public GameObject requiredField1;

[Required("Custom message")]
public Material requiredField2;
```

| Parameter | Description |
| - | - |
| Message | Text to display in the warning |
