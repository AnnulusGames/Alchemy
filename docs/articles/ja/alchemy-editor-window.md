# AlchemyEditorWindow

`EditorWindow`の代わりに`AlchemyEditorWindow`を継承することで、Alchemyの属性を用いてエディタウィンドウを作成することができるようになります。

```cs
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Editor;
using Alchemy.Inspector;

public class EditorWindowExample : AlchemyEditorWindow
{
    [MenuItem("Window/Example")]
    static void Open()
    {
        var window = GetWindow<EditorWindowExample>("Example");
        window.Show();
    }
    
    [Serializable]
    [HorizontalGroup]
    public class DatabaseItem
    {
        [LabelWidth(30f)]
        public float foo;

        [LabelWidth(30f)]
        public Vector3 bar;
        
        [LabelWidth(30f)]
        public GameObject baz;
    }

    [ListViewSettings(ShowAlternatingRowBackgrounds = AlternatingRowBackground.All, ShowFoldoutHeader = false)]
    public List<DatabaseItem> items;

    [Button, HorizontalGroup]
    public void Button1() { }

    [Button, HorizontalGroup]
    public void Button2() { }

    [Button, HorizontalGroup]
    public void Button3() { }
}
```

![img](../../images/img-editor-window.png)

`AlchemyEditorWindow`を継承して作成したウィンドウのデータは、プロジェクトのProjectSettingsフォルダにjson形式で保存されます。詳細は[EditorWindowのデータを保存する](saving-editor-window-data.md)のページを参照してください。