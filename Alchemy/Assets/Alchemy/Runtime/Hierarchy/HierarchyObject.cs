using UnityEngine;

namespace Alchemy.Hierarchy
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Alchemy/Hierarchy Object")]
    public class HierarchyObject : MonoBehaviour
    {
        public enum Mode
        {
            UseSettings = 0,
            RemoveInPlayMode = 1,
            RemoveInBuild = 2
        }

        [SerializeField] Mode hierarchyObjectMode = Mode.UseSettings;
        public Mode HierarchyObjectMode => hierarchyObjectMode;
    }
}