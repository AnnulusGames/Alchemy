using System;
using System.Collections.Generic;
using System.Linq;

namespace Alchemy.Editor
{
    internal static class TypeHelper
    {
        public static object GetDefaultValue(Type type)
        {
            if (!type.IsValueType)
            {
                return null;
            }

            return Activator.CreateInstance(type);
        }

        public static object CreateDefaultInstance(Type type)
        {
            if (type == typeof(string)) return "";
            if (type.IsSubclassOf(typeof(UnityEngine.Object))) return null;
            return Activator.CreateInstance(type);
        }

        public static IEnumerable<Type> GetBaseClassesAndInterfaces(Type type, bool includeSelf = false)
        {
            List<Type> allTypes = new();

            if (includeSelf) allTypes.Add(type);

            if (type.BaseType == typeof(object))
            {
                allTypes.AddRange(type.GetInterfaces());
            }
            else
            {
                allTypes.AddRange(
                    Enumerable.Repeat(type.BaseType, 1)
                        .Concat(type.GetInterfaces())
                        .Concat(GetBaseClassesAndInterfaces(type.BaseType))
                        .Distinct()
                );
            }

            return allTypes;
        }

        public static bool HasDefaultConstructor(Type type)
        {
            return type.GetConstructors().Any(t => t.GetParameters().Count() == 0);
        }
    }
}
