using UnityEngine;

namespace Alchemy.Samples
{
    public class SerializedReferenceSample : MonoBehaviour
    {
        [SerializeReference] public ISample sample;
        [SerializeReference] public ISample[] sampleArray;
    }
}
