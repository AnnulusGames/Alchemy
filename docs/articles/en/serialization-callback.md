# Serialization Callbacks

When using the `[AlchemySerialize]` attribute, the Source Generator automatically implements `ISerializationCallbackReceiver`. Therefore, you cannot use `ISerializationCallbackReceiver` to add callbacks.

Instead, Alchemy provides an alternative interface called `IAlchemySerializationCallbackReceiver`. Please use this interface instead of `ISerializationCallbackReceiver` when using `[AlchemySerialize]`.

```cs
[AlchemySerialize]
public partial class AlchemySerializationSample : MonoBehaviour, IAlchemySerializationCallbackReceiver
{
    public void OnAfterDeserialize()
    {
        Debug.Log("OnAfterDeserialize");
    }

    public void OnBeforeSerialize()
    {
        Debug.Log("OnBeforeSerialize");
    }
}
```