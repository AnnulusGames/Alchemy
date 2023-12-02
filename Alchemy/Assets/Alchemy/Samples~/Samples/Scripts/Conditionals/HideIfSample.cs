using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class HideIfSample : MonoBehaviour
    {
        public bool hide;
        public bool Hide => hide;
        public bool IsHideTrue() => hide;

        [HideIf("hide")] public int hideIfField;
        [HideIf("Hide")] public int hideIfProperty;
        [HideIf("IsHideTrue")] public int hideIfMethod;
    }
}
