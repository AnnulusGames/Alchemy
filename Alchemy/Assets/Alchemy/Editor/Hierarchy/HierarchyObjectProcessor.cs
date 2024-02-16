using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine.SceneManagement;
using Alchemy.Hierarchy;

namespace Alchemy.Editor
{
    public sealed class HierarchyObjectProcessor : IProcessSceneWithReport
    {
        public int callbackOrder => 0;

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            foreach (var obj in scene.GetRootGameObjects()
                .Select(x => x.GetComponent<HierarchyObject>())
                .Where(x => x != null))
            {
                switch (obj.HierarchyObjectMode)
                {
                    case HierarchyObjectMode.None: break;
                    case HierarchyObjectMode.RemoveInPlayMode:
                        obj.OnObjectRemoved();
                        UnityEngine.Object.DestroyImmediate(obj.gameObject);
                        break;
                    case HierarchyObjectMode.RemoveInBuild:
                        if (EditorApplication.isPlaying) break;
                        obj.OnObjectRemoved();
                        UnityEngine.Object.DestroyImmediate(obj.gameObject);
                        break;
                }
            }
        }
    }
}