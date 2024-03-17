using System.Collections.Generic;
using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class LabelTextSample : MonoBehaviour
    {
        [LabelText("FOO!")] public float foo;
        [LabelText("BAR!")] public Vector3 bar;
        [LabelText("BAZ!")] public GameObject baz;

        [LabelText("FOO LIST!")] public List<float> fooList;
        [LabelText("BAR LIST!")] public List<Vector3> barList;
        [LabelText("BAZ LIST!")] public List<GameObject> bazList;
    }
}
