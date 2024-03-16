using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Alchemy.Inspector;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Visual Element that draws properties based on Alchemy attributes
    /// </summary>
    public sealed class AlchemyPropertyField : BindableElement
    {
        public AlchemyPropertyField(SerializedProperty property, Type type, bool isArrayElement = false)
        {
            var labelText = ObjectNames.NicifyVariableName(property.displayName);

            switch (property.propertyType)
            {
                default:
                    element = new PropertyField(property);
                    break;
                case SerializedPropertyType.ObjectReference:
                    if (property.GetAttribute<InlineEditorAttribute>() != null)
                    {
                        element = new InlineEditorObjectField(property, type);
                    }
                    else
                    {
                        element = GUIHelper.CreateObjectPropertyField(property, type);
                    }
                    break;
                case SerializedPropertyType.Generic:
                    var targetType = property.GetPropertyType(isArrayElement);

                    if (InternalAPIHelper.GetDrawerTypeForType(targetType) != null)
                    {
                        element = new PropertyField(property);
                    }
                    else if (property.isArray)
                    {
                        element = new PropertyListView(property);
                    }
                    else if (targetType.TryGetCustomAttribute<PropertyGroupAttribute>(out var groupAttribute)) // custom group
                    {
                        var drawer = AlchemyEditorUtility.CreateGroupDrawer(groupAttribute, targetType);

                        var root = drawer.CreateRootElement(labelText);
                        InspectorHelper.BuildElements(property.serializedObject, root, property.GetValue<object>(), name => property.FindPropertyRelative(name));
                        if (root is BindableElement bindableElement) bindableElement.BindProperty(property);
                        element = root;
                    }
                    else
                    {
                        var foldout = new Foldout() { text = labelText };

                        var clickable = InternalAPIHelper.GetClickable(foldout.Q<Toggle>());
                        InternalAPIHelper.SetAcceptClicksIfDisabled(clickable, true);
                        InspectorHelper.BuildElements(property.serializedObject, foldout, property.GetValue<object>(), name => property.FindPropertyRelative(name));
                        foldout.BindProperty(property);
                        element = foldout;
                    }
                    break;
                case SerializedPropertyType.ManagedReference:
                    element = new SerializeReferenceField(property);
                    break;
            }
            Add(element);
        }

        readonly VisualElement element;

        public VisualElement FieldElement => element;

        public string Label
        {
            get
            {
                return element switch
                {
                    Foldout foldout => foldout.text,
                    PropertyField propertyField => propertyField.label,
                    SerializeReferenceField serializeReferenceField => serializeReferenceField.foldout.text,
                    InlineEditorObjectField inlineEditorObjectField => inlineEditorObjectField.Label,
                    _ => null,
                };
            }
            set
            {
                switch (element)
                {
                    case Foldout foldout:
                        foldout.text = value;
                        break;
                    case PropertyField propertyField:
                        propertyField.label = value;
                        // To reflect changes, simply setting the value to the label property is not enough,
                        // so you need to directly modify the label of elements with a specific class.
                        propertyField.Query<Label>(className: "unity-property-field__label").ForEach(x => x.text = value);
                        break;
                    case SerializeReferenceField serializeReferenceField:
                        serializeReferenceField.foldout.text = value;
                        break;
                    case InlineEditorObjectField inlineEditorObjectField:
                        inlineEditorObjectField.Label = value;
                        break;
                };
            }
        }
    }
}