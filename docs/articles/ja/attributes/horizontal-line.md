# Horizontal Line Attribute

Inspectorに仕切り線を追加します。

![img](../../../images/img-attribute-horizontal-line.png)

```cs
[HorizontalLine]
public float foo;

[HorizontalLine(1f, 0f, 0f)]
public Vector2 bar;

[HorizontalLine(1f, 0.5f, 0f, 0.5f)]
public GameObject baz;
```

| パラメータ | 説明 |
| - | - |
| r | 線の色の赤成分 |
| g | 線の色の緑成分 |
| b | 線の色の青成分 |
| a | 線の色のアルファ値 |