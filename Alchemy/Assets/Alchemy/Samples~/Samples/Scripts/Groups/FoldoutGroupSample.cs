using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class FoldoutGroupSample : MonoBehaviour
    {
        [FoldoutGroup("Group1")] public float foo;
        [FoldoutGroup("Group1")] public Vector3 bar;
        [FoldoutGroup("Group1")] public GameObject baz;

        [FoldoutGroup("Group2")] public float alpha;
        [FoldoutGroup("Group2")] public Vector3 beta;
        [FoldoutGroup("Group2")] public GameObject gamma;
    }
}