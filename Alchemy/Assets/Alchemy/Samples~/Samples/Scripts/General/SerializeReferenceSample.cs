using UnityEngine;

namespace Alchemy.Samples
{
    public class SerializeReferenceSample : MonoBehaviour
    {
        [SerializeReference] public ISample sample;
        [SerializeReference] public ISample[] sampleArray;
    }
}
