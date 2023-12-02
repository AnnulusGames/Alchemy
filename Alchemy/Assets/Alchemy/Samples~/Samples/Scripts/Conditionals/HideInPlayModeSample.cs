using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class HideInPlayModeSample : MonoBehaviour
    {
        [HideInPlayMode] public float foo;
    }
}