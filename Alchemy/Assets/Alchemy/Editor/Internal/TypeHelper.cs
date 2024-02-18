using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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

            return FormatterServices.GetUninitializedObject(type);
        }

        public static object CreateDefaultInstance(Type type)
        {
            if (type == typeof(string)) return "";
            if (type.IsSubclassOf(typeof(UnityEngine.Object))) return null;
            return FormatterServices.GetUninitializedObject(type);
        }

        public static IEnumerable<Type> GetBaseClassesAndInterfaces(Type type, bool includeSelf = false)
        {
            if (includeSelf) yield return type;

            if (type.BaseType == typeof(object))
            {
                foreach (var interfaceType in type.GetInterfaces())
                {
                    yield return interfaceType;
                }
            }
            else
            {
                foreach (var baseType in Enumerable.Repeat(type.BaseType, 1)
                    .Concat(type.GetInterfaces())
                    .Concat(GetBaseClassesAndInterfaces(type.BaseType))
                    .Distinct())
                {
                    yield return baseType;
                }
            }
        }

        public static bool HasDefaultConstructor(Type type)
        {
            return type.GetConstructors().Any(t => t.GetParameters().Count() == 0);
        }
    }
}
