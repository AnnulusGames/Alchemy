# Title Attribute

Inspector上に区切り線付きのヘッダーを表示します。

![img](../../../images/img-attribute-title.png)

```cs
[Title("Title1")]
public float foo;
public Vector3 bar;
public GameObject baz;

[Title("Title2", "Subtitle")]
public float alpha;
public Vector3 beta;
public GameObject gamma;
```

| パラメータ | 説明 |
| - | - |
| Title | ヘッダーに表示するテキスト |
| Subtitle | タイトルの下に小さく表示されるテキスト |