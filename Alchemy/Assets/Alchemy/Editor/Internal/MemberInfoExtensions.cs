using System;
using System.Reflection;

namespace Alchemy.Editor
{
    internal static class MemberInfoExtensions
    {
        public static bool HasCustomAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            return memberInfo.GetCustomAttribute<T>() != null;
        }

        public static bool TryGetCustomAttribute<T>(this MemberInfo memberInfo, out T result) where T : Attribute
        {
            result = memberInfo.GetCustomAttribute<T>();
            return result != null;
        }

        public static bool IsPublic(this MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case MethodInfo methodInfo: return methodInfo.IsPublic;
                case FieldInfo fieldInfo: return fieldInfo.IsPublic;
                case PropertyInfo propertyInfo: return propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsPublic && propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic;
                default: throw new NotSupportedException();
            }
        }
    }
}