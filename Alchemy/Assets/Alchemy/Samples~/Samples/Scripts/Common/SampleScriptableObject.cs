using UnityEngine;

namespace Alchemy.Samples
{
    [CreateAssetMenu(fileName = "NewSampleScriptableObject", menuName = "Alchemy/Samples/Sample Scriptable Object")]
    public sealed class SampleScriptableObject : ScriptableObject
    {
        public float foo;
        public Vector2 bar;
        public GameObject baz;
    }
}
