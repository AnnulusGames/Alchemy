using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class ShowIfSample : MonoBehaviour
    {
        public bool show;
        public bool Show => show;
        public bool IsShowTrue() => show;

        [ShowIf("show")] public int showIfField;
        [ShowIf("Show")] public int showIfProperty;
        [ShowIf("IsShowTrue")] public int showIfMethod;
    }
}
