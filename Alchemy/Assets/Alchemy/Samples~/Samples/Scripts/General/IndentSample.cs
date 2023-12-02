using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class IndentSample : MonoBehaviour
    {
        [Indent] public float foo;
        [Indent(2)] public Vector2 bar;
        [Indent(3)] public GameObject baz;
    }
}
