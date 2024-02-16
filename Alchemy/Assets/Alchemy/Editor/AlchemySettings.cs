using System.IO;
using UnityEngine;
using Alchemy.Hierarchy;

namespace Alchemy.Editor
{
    public sealed class AlchemySettings : ScriptableObject
    {
        static readonly string SettingsPath = "ProjectSettings/AlchemySettings.json";

        static AlchemySettings instance;
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

        public static void SaveSettings()
        {
            File.WriteAllText(SettingsPath, JsonUtility.ToJson(instance, true));
        }

        [SerializeField] HierarchyObjectMode hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        [SerializeField] bool showHierarchyToggles;

        public HierarchyObjectMode HierarchyObjectMode => hierarchyObjectMode;
        public bool ShowHierarchyToggles => showHierarchyToggles;
    }
}