# On Inspector Enable Attribute

Inspectorが有効化された際にメソッドを実行します。

```cs
[OnInspectorEnable]
void OnInspectorEnable()
{
    Debug.Log("Enable");
}
```