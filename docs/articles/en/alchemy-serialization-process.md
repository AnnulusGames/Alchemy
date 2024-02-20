# Alchemy Serialization Process

In Alchemy, by adding the `[AlchemySerialize]` attribute to the target type, a dedicated Source Generator automatically implements `ISerializationCallbackReceiver`. Within this process, all fields annotated with `[AlchemySerializeField]` are gathered, and using the Unity.Serialization package, they are converted to JSON format. However, fields of type `UnityEngine.Object` cannot be handled in JSON format, so their instances are saved in a single list, and only their indices are written to JSON.

For example, consider the following class:

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

[AlchemySerialize]
public partial class AlchemySerializationExample : MonoBehaviour
{
    [AlchemySerializeField, NonSerialized]
    public Dictionary<string, GameObject> dictionary = new();
}
```

Alchemy generates code similar to the following:

```cs
partial class AlchemySerializationExample : global::UnityEngine.ISerializationCallbackReceiver
{
    [global::System.Serializable]
    sealed class AlchemySerializationData
    {
        [global::System.Serializable]
        public sealed class Item
        {
            [global::UnityEngine.HideInInspector] public bool isCreated;
            [global::UnityEngine.TextArea] public string data;
        }

        public Item dictionary = new();

        [global::UnityEngine.SerializeField] private global::System.Collections.Generic.List<UnityEngine.Object> unityObjectReferences = new();

        public global::System.Collections.Generic.IList<UnityEngine.Object> UnityObjectReferences => unityObjectReferences;
    }

    [global::UnityEngine.HideInInspector, global::UnityEngine.SerializeField] private AlchemySerializationData alchemySerializationData =  new();

    void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        if (this is global::Alchemy.Serialization.IAlchemySerializationCallbackReceiver receiver) receiver.OnBeforeSerialize();
        alchemySerializationData.UnityObjectReferences.Clear();
        
        try
        {
            alchemySerializationData.dictionary.data = global::Alchemy.Serialization.Internal.SerializationHelper.ToJson(this.dictionary , alchemySerializationData.UnityObjectReferences);
            alchemySerializationData.dictionary.isCreated = true;
        }
        catch (global::System.Exception ex)
        {
            global::UnityEngine.Debug.LogException(ex);
        }
    }

    void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        try 
        {
            if (alchemySerializationData.dictionary.isCreated)
            {
                this.dictionary = global::Alchemy.Serialization.Internal.SerializationHelper.FromJson<System.Collections.Generic.Dictionary<string, UnityEngine.GameObject>>(alchemySerializationData.dictionary.data, alchemySerializationData.UnityObjectReferences);
            }
        }
        catch (global::System.Exception ex)
        {
            global::UnityEngine.Debug.LogException(ex);
        }

        if (this is global::Alchemy.Serialization.IAlchemySerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
    }
}
```

Using `[AlchemySerializeField]` increases the processing load for serialization and deserialization. Therefore, it is recommended to avoid using `[AlchemySerializeField]` whenever possible.