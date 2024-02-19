# Label Text Attribute

フィールドのラベルのテキストを上書きします。

![img](../../../images/img-attribute-label-text.png)

```cs
[LabelText("FOO!")]
public float foo;

[LabelText("BAR!")]
public Vector3 bar;

[LabelText("BAZ!")]
public GameObject baz;

[Space]
[LabelText("α")]
public float alpha;

[LabelText("β")]
public Vector3 beta;

[LabelText("γ")]
public GameObject gamma;
```

| パラメータ | 説明 |
| - | - |
| Text | フィールドのラベルに表示するテキスト |