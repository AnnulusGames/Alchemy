using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Alchemy.Inspector;

namespace Alchemy.Editor
{
    public static class AlchemyEditorUtility
    {
        public static Type FindGroupDrawerType(PropertyGroupAttribute attribute)
        {
            return TypeCache.GetTypesWithAttribute<CustomGroupDrawerAttribute>()
                .FirstOrDefault(x => x.GetCustomAttribute<CustomGroupDrawerAttribute>().targetAttributeType == attribute.GetType());
        }

        internal static AlchemyGroupDrawer CreateGroupDrawer(PropertyGroupAttribute attribute, Type targetType)
        {
            var drawerType = FindGroupDrawerType(attribute);
            var drawer = (AlchemyGroupDrawer)Activator.CreateInstance(drawerType);
            drawer.SetUniqueId("AlchemyGroupId_" + targetType.FullName + "_" + attribute.GroupPath);
            return drawer;
        }
    }
}