#if ALCHEMY_SUPPORT_SERIALIZATION
using Alchemy.Serialization.Internal;
using NUnit.Framework;
using Unity.Serialization.Json;
using UnityEngine;

namespace Alchemy.Tests.Runtime
{
    public class SerializationTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            JsonSerialization.AddGlobalAdapter(new UnityBuiltinAdapters());
        }

        [Test]
        public void Test_Serialize_Deserialize_AnimationCurve()
        {
            var before = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            var beforeJson = JsonSerialization.ToJson(before);
            Debug.Log(beforeJson);
            var after = JsonSerialization.FromJson<AnimationCurve>(beforeJson);
            var afterJson = JsonSerialization.ToJson(after);
            Debug.Log(afterJson);

            Assert.AreEqual(beforeJson, afterJson);
        }
    }
}
#endif