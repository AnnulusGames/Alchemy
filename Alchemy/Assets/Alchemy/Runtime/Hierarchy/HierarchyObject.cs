using UnityEngine;

namespace Alchemy.Hierarchy
{
    [DisallowMultipleComponent]
    public abstract class HierarchyObject : MonoBehaviour
    {
        [SerializeField] HierarchyObjectMode hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        public HierarchyObjectMode HierarchyObjectMode => hierarchyObjectMode;
    }
}