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
            None = 1,
            RemoveInPlayMode = 2,
            RemoveInBuild = 3
        }

        [SerializeField] Mode hierarchyObjectMode = Mode.UseSettings;
        public Mode HierarchyObjectMode => hierarchyObjectMode;
    }
}