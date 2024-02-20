# On Inspector Destroy Attribute

Inspectorが破棄された際にメソッドを実行します。

```cs
[OnInspectorDestroy]
void OnInspectorDestroy()
{
    Debug.Log("Destroy");
}
```