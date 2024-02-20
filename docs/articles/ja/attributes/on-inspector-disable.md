# On Inspector Disable Attribute

Inspectorが無効化された際にメソッドを実行します。

```cs
[OnInspectorDisable]
void OnInspectorDisable()
{
    Debug.Log("Disable");
}
```