using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class DisableIfSample : MonoBehaviour
    {
        public bool isDisabled;
        public bool IsDisabled => isDisabled;
        public bool IsDisabledMethod() => isDisabled;

        [DisableIf("isDisabled")] public int disableIfField;
        [DisableIf("IsDisabled")] public int disableIfProperty;
        [DisableIf("IsDisabledMethod")] public int disableIfMethod;
    }
}
