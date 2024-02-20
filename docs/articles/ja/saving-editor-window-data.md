# EditorWindowのデータを保存する

`AlchemyEditorWindow`を継承して作成したエディタウィンドウのフィールドのデータは、自動的にプロジェクトのProjectSettingsフォルダ内にjson形式で保存されます。

`SaveWindowData()`, `LoadWindowData()`, `GetWindowDataPath()`をオーバーライドすることで、データの保存/読み込みの処理、保存先のパスを変更することができます。

```cs
using UnityEditor;
using UnityEngine;
using Alchemy.Editor;

public class EditorWindowExample : AlchemyEditorWindow
{
    [MenuItem("Window/Example")]
    static void Open()
    {
        var window = GetWindow<EditorWindowExample>("Example");
        window.Show();
    }

    protected override string GetWindowDataPath()
    {
        // データの保存先のパスを返す
        return ...
    }

    protected override void LoadWindowData(string dataPath)
    {
        // データの読み込み処理を記述
        ...
    }

    protected override void SaveWindowData(string dataPath)
    {
        // データの保存処理を記述
        ...
    }
}
```