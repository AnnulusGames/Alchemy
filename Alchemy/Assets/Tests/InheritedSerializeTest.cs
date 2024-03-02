using System;
using System.Collections.Generic;
using Alchemy.Serialization;
using UnityEngine;


[AlchemySerialize]
public partial class InheritedSerializeTest : InheritedSerializeTestBase<string>
{
    [AlchemySerializeField, NonSerialized] int? nullableInt;
}