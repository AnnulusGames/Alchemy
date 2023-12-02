using System;
using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class ShowInInspectorSample : MonoBehaviour
    {
        [NonSerialized, ShowInInspector] public int field;
        [NonSerialized, ShowInInspector] public SampleClass classField = new();

        [ShowInInspector] public int Getter => 10;
        [field: NonSerialized, ShowInInspector] public string Property { get; set; } = string.Empty;
    }
}