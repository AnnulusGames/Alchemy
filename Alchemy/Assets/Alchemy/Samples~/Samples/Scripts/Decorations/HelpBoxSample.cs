using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class HelpBoxSample : MonoBehaviour
    {
        [HelpBox("Custom Info")]
        public float foo;

        [HelpBox("Custom Warning", HelpBoxMessageType.Warning)]
        public Vector2 bar;

        [HelpBox("Custom Error", HelpBoxMessageType.Error)]
        public GameObject baz;
    }
}
