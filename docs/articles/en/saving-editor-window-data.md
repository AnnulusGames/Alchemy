# Saving EditorWindow Data

The data of an editor window created by inheriting from `AlchemyEditorWindow` is automatically saved in JSON format within the ProjectSettings folder of your project.

You can customize the saving/loading process and the destination path by overriding the `SaveWindowData()`, `LoadWindowData()`, and `GetWindowDataPath()` methods.

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
        // Return the path where the data will be saved
        return ...
    }

    protected override void LoadWindowData(string dataPath)
    {
        // Write the loading process here
        ...
    }

    protected override void SaveWindowData(string dataPath)
    {
        // Write the saving process here
        ...
    }
}
```