# Extending AlchemyEditor

If a MonoBehaviour or ScriptableObject has its own custom editor class, Alchemy attributes won't work by default.
To combine your custom editor extension with Alchemy, you need to inherit from `AlchemyEditor` class instead of the regular `Editor` class.

```cs
using UnityEditor;
using Alchemy.Editor;

[CustomEditor(typeof(Example))]
public class EditorExample : AlchemyEditor
{
    public override VisualElement CreateInspectorGUI()
    {
        // Always call the base CreateInspectorGUI
        base.CreateInspectorGUI();

        // Add your custom logic here
    }
}
```