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
            foreach (var obj in scene.GetRootGameObjects()
                .SelectMany(x => x.GetComponentsInChildren<HierarchyObject>())
                .Where(x => x != null))
            {
                switch (obj.HierarchyObjectMode)
                {
                    case HierarchyObjectMode.None: break;
                    case HierarchyObjectMode.RemoveInPlayMode:
                        OnObjectRemoved(obj);
                        break;
                    case HierarchyObjectMode.RemoveInBuild:
                        if (EditorApplication.isPlaying) break;
                        OnObjectRemoved(obj);
                        break;
                }
            }
        }

        static void OnObjectRemoved(HierarchyObject obj)
        {
            Transform root;
            while (true)
            {
                root = obj.transform.parent;
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

            UnityEngine.Object.DestroyImmediate(obj.gameObject);
        }
    }
}