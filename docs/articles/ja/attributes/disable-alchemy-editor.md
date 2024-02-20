# Disable Alchemy Editor Attribute

対象のクラスのAlchemyEditorを無効化し、デフォルトのInspectorを使用して描画します。フィールドにこの属性を追加した場合、そのフィールドのみをデフォルトのPropertyFieldを用いて描画するように変更します。

![img](../../../images/img-attribute-disable-alchemy-editor.png)

```cs
[DisableAlchemyEditor]
public class DisableAlchemyEditorExample : MonoBehaviour
{
    public float foo;
    public Vector3 bar;
    public GameObject baz;
}
```