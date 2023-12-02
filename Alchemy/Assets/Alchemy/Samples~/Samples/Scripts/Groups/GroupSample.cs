using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class GroupSample : MonoBehaviour
    {
        [Group("Group1")] public float foo;
        [Group("Group1")] public Vector2 bar;
        [Group("Group1")] public GameObject baz;

        [Group("Group2")] public float alpha;
        [Group("Group2")] public Vector2 beta;
        [Group("Group2")] public GameObject gamma;
    }
}
