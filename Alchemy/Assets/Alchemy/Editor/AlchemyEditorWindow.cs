using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    public abstract class AlchemyEditorWindow : EditorWindow
    {
        protected virtual void CreateGUI()
        {
            // Load data
            LoadWindowData(GetWindowDataPath());

            // Create root element and serialized object
            var windowElement = new VisualElement();
            var serializedObject = new SerializedObject(this);

            // Build visual elements and bind serialized object
            InspectorHelper.BuildElements(serializedObject, windowElement, this, name => serializedObject.FindProperty(name), 0);
            windowElement.Bind(serializedObject);

            // Remove "Serialized Data Model Controller" field
            windowElement.hierarchy.RemoveAt(0);

            // Set callback
            windowElement.TrackSerializedObjectValue(serializedObject, so => SaveWindowData(GetWindowDataPath()));

            rootVisualElement.Add(windowElement);
        }

        protected virtual string GetWindowDataPath()
        {
            return $"ProjectSettings/{GetType().FullName}.json";
        }

        protected virtual void SaveWindowData(string dataPath)
        {
            File.WriteAllText(dataPath, JsonUtility.ToJson(this, true));
        }

        protected virtual void LoadWindowData(string dataPath)
        {
            if (File.Exists(dataPath))
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(dataPath), this);
            }
        }
    }
}