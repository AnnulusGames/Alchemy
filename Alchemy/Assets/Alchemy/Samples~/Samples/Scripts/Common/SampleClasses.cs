using System;
using UnityEngine;
using Unity.Properties;

namespace Alchemy.Samples
{
    public interface ISample
    {

    }

    [Serializable]
    [GeneratePropertyBag]
    public sealed class SampleClass : ISample
    {
        public float foo;
        public Vector3 bar;
        public GameObject baz;
    }

    [Serializable]
    public sealed class SampleA : ISample
    {
        public float alpha;
    }

    [Serializable]
    public sealed class SampleB : ISample
    {
        public Vector3 beta;
    }

    [Serializable]
    public sealed class SampleC : ISample
    {
        public GameObject gamma;
    }

    [Serializable]
    public sealed class NestedSample : ISample
    {
        [SerializeReference] public ISample sample;
    }

    [Serializable]
    public sealed class NestedArraySample : ISample
    {
        [SerializeReference] public ISample[] samples;
    }
}