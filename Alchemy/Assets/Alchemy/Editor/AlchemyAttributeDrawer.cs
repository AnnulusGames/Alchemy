using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    public abstract class AlchemyAttributeDrawer
    {
        SerializedObject serializedObject;
        SerializedProperty serializedProperty;
        object target;
        MemberInfo memberInfo;
        Attribute attribute;
        VisualElement targetElement;

        public SerializedObject SerializedObject => serializedObject;
        public SerializedProperty SerializedProperty => serializedProperty;
        public object Target => target;
        public MemberInfo MemberInfo => memberInfo;
        public Attribute Attribute => attribute;
        public VisualElement TargetElement => targetElement;

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
                processor.serializedObject = serializedObject;
                processor.serializedProperty = property;
                processor.target = target;
                processor.memberInfo = memberInfo;
                processor.attribute = attribute;
                processor.targetElement = memberElement;

                processor.OnCreateElement();
            }
        }
    }
}