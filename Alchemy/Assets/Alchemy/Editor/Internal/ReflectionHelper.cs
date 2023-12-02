using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Alchemy.Editor.Internal
{
    public static class ReflectionHelper
    {
        static readonly Dictionary<(Type, string, BindingFlags, bool), FieldInfo> cacheFieldInfo = new();
        static readonly Dictionary<(Type, string, BindingFlags, bool), MethodInfo> cacheMethodInfo = new();
        static readonly Dictionary<(Type, string, BindingFlags, bool), PropertyInfo> cachePropertyInfo = new();
        static readonly Dictionary<(Type, BindingFlags, bool), MemberInfo[]> cacheAllMembers = new();

        static readonly Dictionary<(Type, string), Func<object, object>> cacheGetFieldValue = new();
        static readonly Dictionary<(Type, string), Func<object, object>> cacheGetPropertyValue = new();
        static readonly Dictionary<(Type, string), Func<object, object>> cacheGetMethodValue = new();

        public static Func<object, object> CreateGetter(FieldInfo fieldInfo)
        {
            if (fieldInfo == null) return null;
            if (fieldInfo.IsStatic)
            {
                Expression body = Expression.Convert(Expression.MakeMemberAccess(null, fieldInfo), typeof(object));
                var lambda = Expression.Lambda<Func<object>>(body).Compile();
                return _ => lambda();
            }
            if (fieldInfo.DeclaringType != null)
            {
                var objParam = Expression.Parameter(typeof(object), "obj");
                var tParam = Expression.Convert(objParam, fieldInfo.DeclaringType);
                Expression body = Expression.Convert(Expression.MakeMemberAccess(tParam, fieldInfo), typeof(object));
                return Expression.Lambda<Func<object, object>>(body, objParam).Compile();
            }
            return null;
        }

        public static Func<object, object> CreateGetter(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) return null;
            if (propertyInfo.GetGetMethod(true).IsStatic)
            {
                Expression body = Expression.Convert(Expression.MakeMemberAccess(null, propertyInfo), typeof(object));
                var lambda = Expression.Lambda<Func<object>>(body).Compile();
                return _ => lambda();
            }
            if (propertyInfo.DeclaringType != null)
            {
                var objParam = Expression.Parameter(typeof(object), "obj");
                var tParam = Expression.Convert(objParam, propertyInfo.DeclaringType);
                Expression body = Expression.Convert(Expression.MakeMemberAccess(tParam, propertyInfo), typeof(object));
                return Expression.Lambda<Func<object, object>>(body, objParam).Compile();
            }
            return null;
        }

        public static Func<object, object> CreateGetter(MethodInfo methodInfo)
        {
            if (methodInfo == null) return null;
            if (methodInfo.IsStatic)
            {
                Expression body = Expression.Convert(Expression.Call(null, methodInfo), typeof(object));
                var lambda = Expression.Lambda<Func<object>>(body).Compile();
                return _ => lambda();
            }
            if (methodInfo.DeclaringType != null)
            {
                var objParam = Expression.Parameter(typeof(object), "obj");
                var tParam = Expression.Convert(objParam, methodInfo.DeclaringType);
                Expression body = Expression.Convert(Expression.Call(tParam, methodInfo), typeof(object));
                return Expression.Lambda<Func<object, object>>(body, objParam).Compile();
            }
            return null;
        }

        public static object GetFieldValue(object target, Type type, string name, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            if (!cacheGetFieldValue.TryGetValue((type, name), out var value))
            {
                FieldInfo info = type.GetField(name, bindingAttr);
                value = CreateGetter(info);
                cacheGetFieldValue.Add((type, name), value);
            }
            return value?.Invoke(target);
        }

        public static object GetPropertyValue(object target, Type type, string name, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            if (!cacheGetPropertyValue.TryGetValue((type, name), out var value))
            {
                PropertyInfo info = type.GetProperty(name, bindingAttr);
                value = CreateGetter(info);
                cacheGetPropertyValue.Add((type, name), value);
            }
            return value?.Invoke(target);
        }

        public static object GetMethodValue(object target, Type type, string name, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            if (!cacheGetMethodValue.TryGetValue((type, name), out var value))
            {
                MethodInfo info = type.GetMethod(name, bindingAttr);
                value = CreateGetter(info);
                cacheGetMethodValue.Add((type, name), value);
            }
            return value?.Invoke(target);
        }

        public static FieldInfo GetField(Type type, string name, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = false)
        {
            FieldInfo info;
            if (cacheFieldInfo.ContainsKey((type, name, bindingAttr, inherit)))
            {
                info = cacheFieldInfo[(type, name, bindingAttr, inherit)];
            }
            else
            {
                if (inherit)
                {
                    info = GetAllFieldsIncludingInherited(type, bindingAttr).FirstOrDefault(x => x.Name == name);
                }
                else
                {
                    info = type.GetField(name, bindingAttr);
                }
                cacheFieldInfo.Add((type, name, bindingAttr, inherit), info);
            }
            return info;
        }

        static IEnumerable<FieldInfo> GetAllFieldsIncludingInherited(Type type, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            if (type == null) return Enumerable.Empty<FieldInfo>();
            return type.GetFields(bindingAttr).Concat(GetAllFieldsIncludingInherited(type.BaseType));
        }

        public static PropertyInfo GetProperty(Type type, string name, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = false)
        {
            PropertyInfo info;
            if (cachePropertyInfo.ContainsKey((type, name, bindingAttr, inherit)))
            {
                info = cachePropertyInfo[(type, name, bindingAttr, inherit)];
            }
            else
            {
                if (inherit)
                {
                    info = GetAllPropertiesIncludingInherited(type, bindingAttr).FirstOrDefault(x => x.Name == name);
                }
                else
                {
                    info = type.GetProperty(name, bindingAttr);
                }
                cachePropertyInfo.Add((type, name, bindingAttr, inherit), info);
            }
            return info;
        }

        static IEnumerable<PropertyInfo> GetAllPropertiesIncludingInherited(Type type, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            if (type == null) return Enumerable.Empty<PropertyInfo>();
            return type.GetProperties(bindingAttr).Concat(GetAllPropertiesIncludingInherited(type.BaseType));
        }

        public static MethodInfo GetMethod(Type type, string name, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = false)
        {
            MethodInfo info;
            if (cacheMethodInfo.ContainsKey((type, name, bindingAttr, inherit)))
            {
                info = cacheMethodInfo[(type, name, bindingAttr, inherit)];
            }
            else
            {
                if (inherit)
                {
                    info = GetAllMethodsIncludingInherited(type, bindingAttr).FirstOrDefault(x => x.Name == name);
                }
                else
                {
                    info = type.GetMethods(bindingAttr).Where(x => x.Name == name).FirstOrDefault();
                }
                cacheMethodInfo.Add((type, name, bindingAttr, inherit), info);
            }
            return info;
        }

        static IEnumerable<MethodInfo> GetAllMethodsIncludingInherited(Type type, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            if (type == null) return Enumerable.Empty<MethodInfo>();
            return type.GetMethods(bindingAttr).Concat(GetAllMethodsIncludingInherited(type.BaseType));
        }

        public static object GetValue(object target, string name, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool allowProperty = true, bool allowMethod = true)
        {
            if (target == null) return null;
            Type type = target.GetType();
            object result;

            while (type != null)
            {
                result = GetFieldValue(target, type, name, bindingAttr);
                if (result != null) return result;

                if (allowProperty)
                {
                    result = GetPropertyValue(target, type, name, bindingAttr);
                    if (result != null) return result;
                }

                if (allowMethod)
                {
                    result = GetMethodValue(target, type, name, bindingAttr);
                    if (result != null) return result;
                }

                type = type.BaseType;
            }
            return null;
        }

        public static object GetValue(object target, string name, int index)
        {
            if (GetValue(target, name, allowMethod: false) is not IEnumerable enumerable) return null;
            IEnumerator enumerator = enumerable.GetEnumerator();

            for (int i = 0; i <= index; i++)
            {
                if (!enumerator.MoveNext()) return null;
            }
            return enumerator.Current;
        }

        public static bool GetValueBool(object target, string name)
        {
            if (GetValue(target, name) is bool cond)
            {
                return cond;
            }
            return false;
        }

        public static MemberInfo[] GetMembers(Type type, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, bool inherit = false)
        {
            if (cacheAllMembers.ContainsKey((type, bindingAttr, inherit)))
            {
                return cacheAllMembers[(type, bindingAttr, inherit)];
            }
            else
            {
                MemberInfo[] memberInfos;
                if (inherit)
                {
                    memberInfos = GetMembersIncludingInherited(type, bindingAttr).ToArray();
                }
                else
                {
                    memberInfos = type.GetMembers(bindingAttr);
                }
                cacheAllMembers.Add((type, bindingAttr, inherit), memberInfos);
                return memberInfos;
            }
        }

        static IEnumerable<MemberInfo> GetMembersIncludingInherited(Type type, BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        {
            if (type == null) return Enumerable.Empty<MemberInfo>();
            return type.GetMembers(bindingAttr).Concat(GetMembersIncludingInherited(type.BaseType));
        }

        public static object Invoke(object target, string name, params object[] parameters)
        {
            if (target == null) return false;
            Type type = target.GetType();
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

            while (type != null)
            {
                var m = GetMethod(type, name, bindingAttr);
                if (m != null) return m.Invoke(m.IsStatic ? null : target, parameters);

                type = type.BaseType;
            }
            return false;
        }

        public static object GetCollectionValue(object target, int index)
        {
            var type = target.GetType();

            if (type.IsArray) return ((Array)target).GetValue(index);
            else if (target is IList list) return list[index];

            return null;
        }
    }
}