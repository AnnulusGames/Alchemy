# Serialization Extension

If you want to edit types that Unity cannot serialize normally, such as Dictionary, you can serialize them using the `[AlchemySerialize]` attribute.

To use serialization extension, you need the [Unity.Serialization](https://docs.unity3d.com/Packages/com.unity.serialization@3.1/manual/index.html) package. Also, please note that reflection-based Unity.Serialization serialization may not work in AOT environments prior to Unity 2022.1. Please refer to the package manual for details.

Here is a sample using Alchemy's serialization extension to serialize various types and make them editable in the Inspector:

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization; // Add Alchemy.Serialization namespace

[AlchemySerialize]
public partial class AlchemySerializationExample : MonoBehaviour
{
    // Add the [AlchemySerializeField] and [NonSerialized] attributes to the target field.
    [AlchemySerializeField, NonSerialized]
    public HashSet<GameObject> hashSet = new();

    [AlchemySerializeField, NonSerialized]
    public Dictionary<string, GameObject> dictionary = new();

    [AlchemySerializeField, NonSerialized]
    public (int, int) tuple;

    [AlchemySerializeField, NonSerialized]
    public Vector3? nullable = null;
}
```

![img](../../images/img-serialization-sample.png)

Currently, the following types can be edited in the Inspector:

- Primitive types
- UnityEngine.Object
- AnimationCurve
- Gradient
- Array
- List<>
- HashSet<>
- Dictionary<,>
- ValueTuple<>
- Nullable<>
- class/struct consisting of the above types

For technical details on serialization, please refer to [Alchemy's Serialization Process](alchemy-serialization-process.md).