using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class HorizontalLineSample : MonoBehaviour
    {
        [HorizontalLine]
        public float foo;

        [HorizontalLine(1f, 0f, 0f)]
        public Vector2 bar;

        [HorizontalLine(1f, 0.5f, 0f, 0.5f)]
        public GameObject baz;
    }
}
