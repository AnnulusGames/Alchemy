using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Alchemy.Hierarchy;

namespace Alchemy.Editor
{
    /// <summary>
    /// Alchemy project-level settings
    /// </summary>
    public sealed class AlchemySettings : ScriptableObject
    {
        static readonly string SettingsPath = "ProjectSettings/AlchemySettings.json";

        static AlchemySettings instance;

        /// <summary>
        /// Get a cached instance. If the cache does not exist, returns a newly created one.
        /// </summary>
        public static AlchemySettings GetOrCreateSettings()
        {
            if (instance != null) return instance;

            if (File.Exists(SettingsPath))
            {
                instance = CreateInstance<AlchemySettings>();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(SettingsPath), instance);
            }
            else
            {
                instance = CreateInstance<AlchemySettings>();
            }

            return instance;
        }

        /// <summary>
        /// Save the settings to a file.
        /// </summary>
        public static void SaveSettings()
        {
            File.WriteAllText(SettingsPath, JsonUtility.ToJson(instance, true));
        }

        static readonly string SettingsMenuName = "Project/Alchemy";

        [SettingsProvider]
        internal static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider(SettingsMenuName, SettingsScope.Project)
            {
                label = "Alchemy",
                keywords = new HashSet<string>(new[] { "Alchemy, Inspector, Hierarchy" }),
                guiHandler = searchContext =>
                {
                    var serializedObject = new SerializedObject(GetOrCreateSettings());

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
                                    SaveSettings();
                                }
                            }
                        }
                    }
                },
            };
        }

        [SerializeField] HierarchyObjectMode hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        [SerializeField] bool showHierarchyToggles;
        [SerializeField] bool showComponentIcons;

        public HierarchyObjectMode HierarchyObjectMode => hierarchyObjectMode;
        public bool ShowHierarchyToggles => showHierarchyToggles;
        public bool ShowComponentIcons => showComponentIcons;
    }
}