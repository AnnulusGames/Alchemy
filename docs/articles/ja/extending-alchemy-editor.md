# AlchemyEditorを拡張する

対象のMonoBehaviourやScriptableObjectが独自のEditorクラスを持つ場合、Alchemyの属性は動作しません。
独自のエディタ拡張とAlchemyを組み合わせたい場合には、通常の`Editor`クラスではなく`AlchemyEditor`クラスを継承する必要があります。

```cs
using UnityEditor;
using Alchemy.Editor;

[CustomEditor(typeof(Example))]
public class EditorExample : AlchemyEditor
{
    public override VisualElement CreateInspectorGUI()
    {
        // 必ず継承元のCreateInspectorGUIを呼び出す
        base.CreateInspectorGUI();

        // ここに独自の処理を記述する
    }
}
```