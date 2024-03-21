using System;
using Alchemy.Serialization;
using UnityEngine;

namespace Test
{
    [ShowAlchemySerializationData]
    [AlchemySerialize]
    public partial class InheritedSerializeTest : InheritedSerializeTestBase<string>
    {
        [AlchemySerializeField, NonSerialized] int? nullableInt;
    }
}