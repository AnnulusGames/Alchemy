using System;

namespace Alchemy.Editor
{
    public sealed class CustomGroupDrawerAttribute : Attribute
    {
        public CustomGroupDrawerAttribute(Type targetAttributeType)
        {
            this.targetAttributeType = targetAttributeType;
        }
        public readonly Type targetAttributeType;
    }
}