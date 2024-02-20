# Show In Inspector Attribute

シリアライズされていないフィールドやプロパティをInspectorから編集可能にします。これらの値はシリアライズされず、変更は保存されないことに留意してください。

![img](../../../images/img-attribute-show-in-inspector.png)

```cs 
[NonSerialized, ShowInInspector]
public int field;

[NonSerialized, ShowInInspector]
public SampleClass classField = new();

[ShowInInspector]
public int Getter => 10;

[field: NonSerialized, ShowInInspector]
public string Property { get; set; } = string.Empty;
```
