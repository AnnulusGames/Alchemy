# シリアル化コールバック

`[AlchemySerialize]`属性を使用するとSource Generatorが`ISerializationCallbackReceiver`を実装するため、通常通り`ISerializationCallbackReceiver`を使用してコールバックを追加することができません。

そのため、Alchemyでは代替となるインターフェースとして`IAlchemySerializationCallbackReceiver`を提供しています。`[AlchemySerialize]`を使用する際には`ISerializationCallbackReceiver`の代わりにこちらを利用してください。

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