using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class InlineEditorSample : MonoBehaviour
    {
        [InlineEditor] public SampleScriptableObject sample;
    }
}