using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class BoxGroupSample : MonoBehaviour
    {
        [BoxGroup("Group1")] public float foo;
        [BoxGroup("Group1")] public Vector2 bar;
        [BoxGroup("Group1")] public GameObject baz;

        [BoxGroup("Group1/Group2")] public float alpha;
        [BoxGroup("Group1/Group2")] public Vector2 beta;
        [BoxGroup("Group1/Group2")] public GameObject gamma;
    }
}
