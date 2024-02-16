using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Alchemy.Hierarchy;

namespace Alchemy.Editor
{
    public sealed class HierarchyObjectProcessor : IProcessSceneWithReport
    {
        public int callbackOrder => 0;

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            var settings = AlchemySettings.GetOrCreateSettings();

            var hierarchyObjects = scene.GetRootGameObjects()
                .SelectMany(x => x.GetComponentsInChildren<HierarchyObject>())
                .Where(x => x != null)
                .ToArray();

            foreach (var obj in hierarchyObjects)
            {
                switch (GetHierarchyObjectMode(obj))
                {
                    case HierarchyObjectMode.None: break;
                    case HierarchyObjectMode.RemoveInPlayMode:
                        PreRemove(obj);
                        break;
                    case HierarchyObjectMode.RemoveInBuild:
                        if (EditorApplication.isPlaying) break;
                        PreRemove(obj);
                        break;
                }
            }

            foreach (var obj in hierarchyObjects)
            {
                if (obj == null) continue;

                switch (GetHierarchyObjectMode(obj))
                {
                    case HierarchyObjectMode.None: break;
                    case HierarchyObjectMode.RemoveInPlayMode:
                        Object.DestroyImmediate(obj.gameObject);
                        break;
                    case HierarchyObjectMode.RemoveInBuild:
                        if (EditorApplication.isPlaying) break;
                        Object.DestroyImmediate(obj.gameObject);
                        break;
                }
            }
        }

        static HierarchyObjectMode GetHierarchyObjectMode(HierarchyObject obj)
        {
            return obj.HierarchyObjectMode != HierarchyObject.Mode.UseSettings
                ? (HierarchyObjectMode)obj.HierarchyObjectMode
                : AlchemySettings.GetOrCreateSettings().HierarchyObjectMode;
        }

        static void PreRemove(HierarchyObject obj)
        {
            if (obj == null) return;

            Transform root = obj.transform;
            while (true)
            {
                root = root.parent;
                if (root == null) goto LOOP_END;
                if (!root.TryGetComponent<HierarchyObject>(out var hierarchyObject)) goto LOOP_END;

                switch (GetHierarchyObjectMode(hierarchyObject))
                {
                    case HierarchyObjectMode.None:
                        goto LOOP_END;
                    case HierarchyObjectMode.RemoveInPlayMode:
                        break;
                    case HierarchyObjectMode.RemoveInBuild:
                        if (EditorApplication.isPlaying) goto LOOP_END;
                        break;
                }
            }

        LOOP_END:
            foreach (Transform child in obj.transform)
            {
                child.SetParent(root);
            }
        }
    }
}