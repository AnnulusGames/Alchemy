using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class BlockquoteSample : MonoBehaviour
    {
        [Blockquote("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.")]
        public float foo;
    }
}
