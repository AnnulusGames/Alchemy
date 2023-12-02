using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class LabelTextSample : MonoBehaviour
    {
        [LabelText("FOO!")] public float foo;
        [LabelText("BAR!")] public Vector3 bar;
        [LabelText("BAZ!")] public GameObject baz;
    }
}
