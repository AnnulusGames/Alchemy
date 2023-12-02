using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class TabGroupSample : MonoBehaviour
    {
        [TabGroup("Group1", "Tab1")] public float foo;
        [TabGroup("Group1", "Tab2")] public Vector3 bar;
        [TabGroup("Group1", "Tab3")] public GameObject baz;

        [TabGroup("Group1", "Tab1")] public float alpha;
        [TabGroup("Group1", "Tab2")] public Vector3 beta;
        [TabGroup("Group1", "Tab3")] public GameObject gamma;
    }
}
