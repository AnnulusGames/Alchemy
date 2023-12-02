using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class AssetsOnlySample : MonoBehaviour
    {
        [AssetsOnly] public Object asset1;
        [AssetsOnly] public GameObject asset2;
    }
}
