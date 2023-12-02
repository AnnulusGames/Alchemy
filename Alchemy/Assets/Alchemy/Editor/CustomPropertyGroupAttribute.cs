using System;

namespace Alchemy.Editor
{
    public sealed class CustomPropertyGroupDrawerAttribute : Attribute
    {
        public CustomPropertyGroupDrawerAttribute(Type targetAttributeType)
        {
            this.targetAttributeType = targetAttributeType;
        }
        public readonly Type targetAttributeType;
    }
}