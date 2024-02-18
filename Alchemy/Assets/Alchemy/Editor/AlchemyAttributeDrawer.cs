using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    public abstract class AlchemyAttributeDrawer
    {
        internal SerializedObject _serializedObject;
        internal SerializedProperty _serializedProperty;
        internal object _target;
        internal MemberInfo _memberInfo;
        internal Attribute _attribute;
        internal VisualElement _targetElement;

        public SerializedObject SerializedObject => _serializedObject;
        public SerializedProperty SerializedProperty => _serializedProperty;
        public object Target => _target;
        public MemberInfo MemberInfo => _memberInfo;
        public Attribute Attribute => _attribute;
        public VisualElement TargetElement => _targetElement;

        public abstract void OnCreateElement();

        internal static void ExecutePropertyDrawers(SerializedObject serializedObject, SerializedProperty property, object target, MemberInfo memberInfo, VisualElement memberElement)
        {
            var attributes = memberInfo.GetCustomAttributes();
            var processorTypes = TypeCache.GetTypesWithAttribute(typeof(CustomAttributeDrawerAttribute));
            foreach (var attribute in attributes)
            {
                var processorType = processorTypes.FirstOrDefault(x => x.IsSubclassOf(typeof(AlchemyAttributeDrawer)) && x.GetCustomAttribute<CustomAttributeDrawerAttribute>().targetAttributeType == attribute.GetType());
                if (processorType == null) continue;

                var processor = (AlchemyAttributeDrawer)Activator.CreateInstance(processorType);
                processor._serializedObject = serializedObject;
                processor._serializedProperty = property;
                processor._target = target;
                processor._memberInfo = memberInfo;
                processor._attribute = attribute;
                processor._targetElement = memberElement;

                processor.OnCreateElement();
            }
        }
    }
}