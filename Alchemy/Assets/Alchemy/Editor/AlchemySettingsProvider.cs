using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    internal static class AlchemySettingsProvider
    {
        static readonly string MenuName = "Project/Alchemy";

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            static Label CreateHeader(string text)
            {
                return new Label(text)
                {
                    style = {
                        unityFontStyleAndWeight = FontStyle.Bold,
                        marginLeft = 2f,
                        marginTop = 3f,
                    }
                };
            }

            return new SettingsProvider(MenuName, SettingsScope.Project)
            {
                keywords = new HashSet<string>(new[] { "Alchemy, Inspector, Hierarchy" }),
                activateHandler = (searchContext, rootElement) =>
                {
                    var serializedObject = new SerializedObject(AlchemySettings.GetOrCreateSettings());

                    var root = new VisualElement
                    {
                        style = {
                            marginLeft = 10f
                        }
                    };
                    rootElement.Add(root);

                    var label = new Label("Alchemy")
                    {
                        style = {
                            unityFontStyleAndWeight = FontStyle.Bold,
                            fontSize = 20,
                            marginTop = 3f,
                            marginBottom = 5f
                        }
                    };
                    root.Add(label);

                    var hierarchyHeader = CreateHeader("Hierarchy");
                    root.Add(hierarchyHeader);

                    var hierarchyObjectModeField = new PropertyField(serializedObject.FindProperty("hierarchyObjectMode"));
                    root.Add(hierarchyObjectModeField);

                    var showHierarchyTogglesField = new PropertyField(serializedObject.FindProperty("showHierarchyToggles"))
                    {
                        label = "Show Toggles"
                    };
                    root.Add(showHierarchyTogglesField);

                    root.Bind(serializedObject);
                    root.TrackSerializedObjectValue(serializedObject, so =>
                    {
                        AlchemySettings.SaveSettings();
                    });
                },
            };
        }
    }
}