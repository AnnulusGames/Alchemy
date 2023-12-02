using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

namespace Alchemy.Samples
{
    [AlchemySerialize]
    [ShowAlchemySerializationData]
    public partial class ShowAlchemySerializationDataSample : MonoBehaviour
    {
        [AlchemySerializeField, NonSerialized]
        public Dictionary<string, GameObject> dictionary = new();
    }
}
