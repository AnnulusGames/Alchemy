# Debugging Serialized Data

By adding the `[ShowAlchemySerializationData]` attribute along with `[AlchemySerialize]`, you can inspect serialized data from the Inspector.

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

[AlchemySerialize]
[ShowAlchemySerializationData]
public partial class AlchemySerializationExample : MonoBehaviour
{
    [AlchemySerializeField, NonSerialized]
    public Dictionary<string, GameObject> dictionary = new();
}
```

![img](../../images/img-show-serialization-data.png)
