using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class RequiredSample : MonoBehaviour
    {
        [Required]
        public GameObject requiredField1;

        [Required("Custom message")]
        public Material requiredField2;
    }
}