# Alchemyのシリアル化プロセス

Alchemyでは、対象の型に`[AlchemySerialize]`属性を付加することで専用のSource Generatorが`ISerializationCallbackReceiver`を自動で実装します。この処理の中で`[AlchemySerializeField]`属性が付加されたフィールドを全て取得し、Unity.Serializationパッケージを用いてJson形式に変換します。ただし、UnityEngine.Objectのフィールドに関してはJson形式で扱うことができないため、単一のListに実体を保存しJsonにはそのindexを書き込みます。

例えば、以下のようなクラスがあったとします。

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

これに対し、Alchemyは以下のようなコードを生成します。

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

このような手法を取っているため`[AlchemySerializeField]`を使用するとシリアライズ/デシリアライズにかかる処理負荷が増加します。そのため可能な限り`[AlchemySerializeField]`の使用を避けることが推奨されます。
