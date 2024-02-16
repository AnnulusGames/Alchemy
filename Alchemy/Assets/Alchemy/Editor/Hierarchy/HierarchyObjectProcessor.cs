using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine.SceneManagement;
using Alchemy.Hierarchy;
using UnityEngine;

namespace Alchemy.Editor
{
    public sealed class HierarchyObjectProcessor : IProcessSceneWithReport
    {
        public int callbackOrder => 0;

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            var hierarchyObjects = scene.GetRootGameObjects()
                .SelectMany(x => x.GetComponentsInChildren<HierarchyObject>())
                .Where(x => x != null)
                .ToArray();
            
            foreach (var obj in hierarchyObjects)
            {
                switch (obj.HierarchyObjectMode)
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
                switch (obj.HierarchyObjectMode)
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

        static void PreRemove(HierarchyObject obj)
        {
            if (obj == null) return;

            Transform root = obj.transform;
            while (true)
            {
                root = root.parent;
                if (root == null) goto LOOP_END;
                if (!root.TryGetComponent<HierarchyObject>(out var hierarchyObject)) goto LOOP_END;

                switch (hierarchyObject.HierarchyObjectMode)
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