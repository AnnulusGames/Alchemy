# Group Attribute

複数のメンバーをまとめて表示するグループを作成します。

![img](../../../images/img-attribute-group.png)

```cs 
[Group("Group1")]
public float foo;

[Group("Group1")]
public Vector3 bar;

[Group("Group1")]
public GameObject baz;

[Group("Group2")]
public float alpha;

[Group("Group2")]
public Vector3 beta;

[Group("Group2")]
public GameObject gamma;
```

| パラメータ | 説明 |
| - | - |
| GroupPath | グループのパスを指定します。グループは`/`で区切ることでネストすることが可能です。 |
