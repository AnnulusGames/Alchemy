using System;

namespace Alchemy.Editor
{
    public sealed class CustomPropertyProcessorAttribute : Attribute
    {
        public CustomPropertyProcessorAttribute(Type targetAttributeType, int order = 0)
        {
            this.targetAttributeType = targetAttributeType;
            this.order = order;
        }
        
        public readonly Type targetAttributeType;
        public readonly int order;
    }
}
