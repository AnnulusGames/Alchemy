# On Value Changed Attribute

フィールドの値が変更された際に、指定された名前のメソッドを実行します。

```cs 
[OnValueChanged("OnValueChanged")]
public int foo;

void OnValueChanged(int value)
{
    Debug.Log(value);
}
```

| パラメータ | 説明 |
| - | - |
| MethodName | 値の変更時に呼ばれるメソッドの名前 |