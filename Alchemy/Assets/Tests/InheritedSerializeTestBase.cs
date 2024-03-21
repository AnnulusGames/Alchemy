using System;
using System.Collections.Generic;
using Alchemy.Serialization;
using UnityEngine;

[ShowAlchemySerializationData]
[AlchemySerialize]
public partial class InheritedSerializeTestBase<T> : MonoBehaviour
{
    [AlchemySerializeField, NonSerialized] HashSet<T> set;
}

