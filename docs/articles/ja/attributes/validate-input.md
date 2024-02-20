# Validate Input Attribute

指定された名前のメンバーの値がfalseである場合に警告を表示します。

![img](../../../images/img-attribute-validate-input.png)

```cs 
[ValidateInput("IsNotNull")]
public GameObject obj;

[ValidateInput("IsZeroOrGreater", "foo must be 0 or greater.")]
public int foo = -1;

static bool IsNotNull(GameObject go)
{
    return go != null;
}

static bool IsZeroOrGreater(int a)
{
    return a >= 0;
}
```

| パラメータ | 説明 |
| - | - |
| Condition | 値の検証に使用するフィールド、プロパティまたはメソッドの名前 |
| Message | 警告に表示するテキスト |