using System;

namespace Alchemy.Editor
{
    /// <summary>
    /// Attribute for specifying the target attribute of AlchemyAttributeDrawer.
    /// </summary>
    public sealed class CustomAttributeDrawerAttribute : Attribute
    {
        public CustomAttributeDrawerAttribute(Type targetAttributeType, int order = 0)
        {
            this.targetAttributeType = targetAttributeType;
            this.order = order;
        }

        public readonly Type targetAttributeType;
        public readonly int order;
    }
}
