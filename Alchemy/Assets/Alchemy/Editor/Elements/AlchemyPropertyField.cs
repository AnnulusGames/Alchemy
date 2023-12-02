using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Alchemy.Editor.Internal;
using Alchemy.Inspector;
using UnityEngine;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Visual Element that draws properties based on Alchemy attributes
    /// </summary>
    public sealed class AlchemyPropertyField : BindableElement
    {
        public AlchemyPropertyField(SerializedProperty property, int depth)
        {
            if (depth > 20) return;
            var labelText = ObjectNames.NicifyVariableName(property.displayName);

            switch (property.propertyType)
            {
                default:
                    element = new PropertyField(property);
                    break;
                case SerializedPropertyType.ObjectReference:
                    if (property.GetAttribute<InlineEditorAttribute>() != null)
                    {
                        element = new InlineEditorObjectField(property, depth);
                    }
                    else
                    {
                        element = GUIHelper.CreateObjectField(property);
                    }
                    break;
                case SerializedPropertyType.Generic:
                    if (property.isArray)
                    {
                        element = new PropertyListView(property, depth);
                    }
                    else
                    {
                        var targetType = property.GetPropertyType();
                        var foldout = new Foldout() { text = labelText };

                        var clickable = InternalAPIHelper.GetClickable(foldout.Q<Toggle>());
                        InternalAPIHelper.SetAcceptClicksIfDisabled(clickable, true);
                        InspectorHelper.BuildElements(property.serializedObject, foldout, property.GetValue<object>(), name => property.FindPropertyRelative(name), depth + 1);
                        foldout.BindProperty(property);
                        element = foldout;
                    }
                    break;
                case SerializedPropertyType.ManagedReference:
                    element = new SerializeReferenceField(property, depth);
                    break;
            }
            Add(element);
        }

        readonly VisualElement element;

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