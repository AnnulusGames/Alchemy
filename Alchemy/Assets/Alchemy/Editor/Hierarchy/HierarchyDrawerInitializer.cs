using System;
using System.Linq;
using UnityEditor;

namespace Alchemy.Editor
{
    internal static class HierarchyDrawerInitializer
    {
        [InitializeOnLoadMethod]
        static void Init()
        {
            var drawers = TypeCache.GetTypesDerivedFrom<HierarchyDrawer>()
                .Where(x => !x.IsAbstract)
                .Select(x => (HierarchyDrawer)Activator.CreateInstance(x));

            foreach (var drawer in drawers)
            {
                EditorApplication.hierarchyWindowItemOnGUI += drawer.OnGUI;
            }
        }
    }
}