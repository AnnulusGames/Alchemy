using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class HideLabelSample : MonoBehaviour
    {
        [HideLabel] public float foo;
        [HideLabel] public Vector3 bar;
        [HideLabel] public GameObject baz;
    }
}
