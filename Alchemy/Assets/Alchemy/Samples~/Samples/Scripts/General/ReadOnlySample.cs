using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class ReadOnlySample : MonoBehaviour
    {
        [ReadOnly] public float field = 2.5f;
        [ReadOnly] public int[] array = new int[5];
        [ReadOnly] public SampleClass classField;
        [ReadOnly] public SampleClass[] classArray = new SampleClass[3];
    }
}
