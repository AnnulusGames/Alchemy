using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;

namespace Alchemy.Editor.Elements
{
    public sealed class ReflectionField : VisualElement
    {
        public ReflectionField(IObjectAccess objectAccess, MemberInfo memberInfo, int depth)
        {
            Rebuild(objectAccess, memberInfo, depth);
        }

        public void Rebuild(IObjectAccess objectAccess, MemberInfo memberInfo, int depth)
        {
            Clear();

            if (memberInfo is MethodInfo methodInfo)
            {
                if (methodInfo.HasCustomAttribute<ButtonAttribute>())
                {
                    var button = new MethodButton(objectAccess, methodInfo);
                    Add(button);
                }
                return;
            }

            var currentTarget = objectAccess is IdentityAccess ? objectAccess.Target : null;
            object value;
            GenericField element;
            switch (memberInfo)
            {
                default: return;
                case FieldInfo fieldInfo:
                    value = fieldInfo.IsStatic ? fieldInfo.GetValue(null) : currentTarget == null ? TypeHelper.GetDefaultValue(fieldInfo.FieldType) : fieldInfo.GetValue(currentTarget);
                    var fieldType = currentTarget == null ? fieldInfo.FieldType : fieldInfo.GetValue(currentTarget)?.GetType() ?? fieldInfo.FieldType;
                    element = new GenericField(new IdentityAccess( value), fieldType, ObjectNames.NicifyVariableName(memberInfo.Name), depth, true);
                    element.OnValueChanged += x =>
                    {
                        var target = objectAccess.Target;
                        OnBeforeValueChange?.Invoke(target);
                        fieldInfo.SetValue(target, x);

                        // Force serialization
                        if (target is ISerializationCallbackReceiver receiver)
                        {
                            receiver.OnBeforeSerialize();
                        }

                        OnValueChanged?.Invoke(target);
                        objectAccess.Target = target;
                    };
                    break;
                case PropertyInfo propertyInfo:
                    if (!propertyInfo.HasCustomAttribute<ShowInInspectorAttribute>()) return;
                    if (!propertyInfo.CanRead) return;

                    value = propertyInfo.GetMethod.IsStatic ? propertyInfo.GetValue(null) : currentTarget == null ? TypeHelper.GetDefaultValue(propertyInfo.PropertyType) : propertyInfo.GetValue(currentTarget);
                    var propertyType = currentTarget == null ? propertyInfo.PropertyType : propertyInfo.GetValue(currentTarget)?.GetType() ?? propertyInfo.PropertyType;
                    element = new GenericField(new IdentityAccess( value), propertyType, ObjectNames.NicifyVariableName(memberInfo.Name), depth, true);
                    element.OnValueChanged += x =>
                    {
                        var target = objectAccess.Target;
                        OnBeforeValueChange?.Invoke(target);
                        if (propertyInfo.CanWrite) propertyInfo.SetValue(target, x);

                        // Force serialization
                        if (target is ISerializationCallbackReceiver receiver)
                        {
                            receiver.OnBeforeSerialize();
                        }

                        OnValueChanged?.Invoke(target);
                        objectAccess.Target = target;
                    };
                    element.SetEnabled(propertyInfo.CanWrite);
                    break;
            }

            Add(element);
        }

        public event Action<object> OnBeforeValueChange;
        public event Action<object> OnValueChanged;
    }
}