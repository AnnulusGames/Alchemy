# Show If Attribute

対象のメンバーのbool値がtrueの場合にのみInspectorに表示されます。

![img](../../../images/img-attribute-show-if-false.png)

![img](../../../images/img-attribute-show-if-true.png)

```cs
public bool show;

public bool Show => show;
public bool IsShowTrue() => show;

[ShowIf("show")]
public int showIfField;

[ShowIf("Show")]
public int showIfProperty;

[ShowIf("IsShowTrue")]
public int showIfMethod;
```


| パラメータ | 説明 |
| - | - |
| Condition | 条件の判定に使用するフィールド、プロパティまたはメソッドの名前 |
