using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Alchemy.Editor
{
    internal static class AlchemySettingsProvider
    {
        static readonly string MenuName = "Project/Alchemy";
        static SerializedObject serializedObject;

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider(MenuName, SettingsScope.Project)
            {
                label = "Alchemy",
                keywords = new HashSet<string>(new[] { "Alchemy, Inspector, Hierarchy" }),
                guiHandler = searchContext =>
                {
                    if (serializedObject == null) serializedObject = new(AlchemySettings.GetOrCreateSettings());
                    else serializedObject.Update();

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Space(10f);
                        using (new EditorGUILayout.VerticalScope())
                        {
                            EditorGUILayout.LabelField("Hierarchy", EditorStyles.boldLabel);

                            using (var changeCheck = new EditorGUI.ChangeCheckScope())
                            {
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyObjectMode"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("showHierarchyToggles"), new GUIContent("Show Toggles"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("showComponentIcons"));

                                if (changeCheck.changed)
                                {
                                    serializedObject.ApplyModifiedProperties();
                                    AlchemySettings.SaveSettings();
                                }
                            }
                        }
                    }
                },
            };
        }
    }
}