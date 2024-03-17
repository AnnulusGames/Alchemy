using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class PreviewSample : MonoBehaviour
    {
        [Preview] public Sprite foo;
        [Preview] public Texture bar;
        [Preview] public Material baz;
        [Preview] public GameObject qux;
    }
}
