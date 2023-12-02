using System;
using System.Collections.Generic;
using UnityEngine;
#if ALCHEMY_SUPPORT_SERIALIZATION
using Alchemy.Serialization;
#endif

namespace Alchemy.Samples
{
#if ALCHEMY_SUPPORT_SERIALIZATION
    [AlchemySerialize]
#endif
    public partial class AlchemySerializationSample : MonoBehaviour
    {
#if ALCHEMY_SUPPORT_SERIALIZATION
        [AlchemySerializeField, NonSerialized]
        public HashSet<GameObject> hashset = new();

        [AlchemySerializeField, NonSerialized]
        public Dictionary<string, GameObject> dictionary = new();

        [AlchemySerializeField, NonSerialized]
        public (int, int) tuple;

        [AlchemySerializeField, NonSerialized]
        public Vector3? nullable = null;
#endif
    }
}
