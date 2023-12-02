using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class ValidateInputSample : MonoBehaviour
    {
        [ValidateInput("IsNotNull")]
        public GameObject obj;

        [ValidateInput("IsZeroOrGreater", "foo must be 0 or greater.")]
        public int foo = -1;

        static bool IsNotNull(GameObject go)
        {
            return go != null;
        }

        static bool IsZeroOrGreater(int a)
        {
            return a >= 0;
        }
    }
}
