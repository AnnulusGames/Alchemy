using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class EnableIfSample : MonoBehaviour
    {
        public bool isEnabled;
        public bool IsEnabled => isEnabled;
        public bool IsEnabledMethod() => isEnabled;

        [EnableIf("isEnabled")] public int enableIfField;
        [EnableIf("IsEnabled")] public int enableIfProperty;
        [EnableIf("IsEnabledMethod")] public int enableIfMethod;
    }
}
