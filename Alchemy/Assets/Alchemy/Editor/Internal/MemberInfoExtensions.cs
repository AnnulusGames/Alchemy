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
    }
}