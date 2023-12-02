using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class BasicAttributesSample : MonoBehaviour
    {
        [LabelText("Custom Label")]
        public float foo;

        [HideLabel]
        public Vector3 bar;
        
        [AssetsOnly]
        public GameObject baz;

        [Title("Title")]
        [HelpBox("HelpBox", HelpBoxMessageType.Info)]
        [ReadOnly]
        public string message = "Read Only";
    }
}
