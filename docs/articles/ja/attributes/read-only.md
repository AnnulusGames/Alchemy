# Read Only Attribute

フィールドを編集不可能にします。

![img](../../../images/img-attribute-read-only.png)

```cs
[ReadOnly]
public float field = 2.5f;

[ReadOnly]
public int[] array = new int[5];

[ReadOnly]
public SampleClass classField;

[ReadOnly]
public SampleClass[] classArray = new SampleClass[3];
```
