# Tab Group Attribute

複数のメンバーをタブに分割するグループを作成します。

![img](../../../images/img-attribute-tab-group.png)

```cs 
[TabGroup("Group1", "Tab1")]
public float foo;

[TabGroup("Group1", "Tab2")]
public Vector3 bar;

[TabGroup("Group1", "Tab3")]
public GameObject baz;

[TabGroup("Group1", "Tab1")]
public float alpha;

[TabGroup("Group1", "Tab2")]
public Vector3 beta;

[TabGroup("Group1", "Tab3")]
public GameObject gamma;
```

| パラメータ | 説明 |
| - | - |
| GroupPath | グループのパスを指定します。グループは`/`で区切ることでネストすることが可能です。 |
| TabName | メンバーが所属するタブの名前を指定します。 |
