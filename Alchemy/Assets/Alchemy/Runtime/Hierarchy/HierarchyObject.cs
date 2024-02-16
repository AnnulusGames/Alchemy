using UnityEngine;

namespace Alchemy.Hierarchy
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Alchemy/Hierarchy Object")]
    public class HierarchyObject : MonoBehaviour
    {
        [SerializeField] HierarchyObjectMode hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        public HierarchyObjectMode HierarchyObjectMode => hierarchyObjectMode;
    }
}