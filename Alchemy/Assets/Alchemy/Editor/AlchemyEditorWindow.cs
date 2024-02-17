using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    public abstract class AlchemyEditorWindow : EditorWindow
    {
        protected virtual void CreateGUI()
        {
            var windowElement = new VisualElement();
            var serializedObject = new SerializedObject(this);
            InspectorHelper.BuildElements(serializedObject, windowElement, this, name => serializedObject.FindProperty(name), 0);
            windowElement.Bind(serializedObject);

            // Remove "Serialized Data Model Controller" field
            windowElement.hierarchy.RemoveAt(0);

            rootVisualElement.Add(windowElement);
        }
    }
}