using System;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Visual Element that draws the ObjectField of the InlineEditor attribute
    /// </summary>
    public sealed class InlineEditorObjectField : BindableElement
    {
        public InlineEditorObjectField(SerializedProperty property, Type type, int depth)
        {
            Assert.IsTrue(property.propertyType == SerializedPropertyType.ObjectReference);

            style.minHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            foldout = new Foldout()
            {
                text = ObjectNames.NicifyVariableName(property.displayName)
            };
            var toggle = foldout.Q<Toggle>();
            var clickable = InternalAPIHelper.GetClickable(toggle);
            InternalAPIHelper.SetAcceptClicksIfDisabled(clickable, true);

            foldout.BindProperty(property);

            field = GUIHelper.CreateObjectField(property, type);
            field.style.position = Position.Absolute;
            field.style.width = Length.Percent(100f);

            field.RegisterValueChangeCallback(x =>
            {
                isNull = x.changedProperty.objectReferenceValue == null;

                field.Q<Label>().text = isNull ? ObjectNames.NicifyVariableName(property.displayName) : string.Empty;
                field.pickingMode = PickingMode.Ignore;

                var objectField = field.Q<ObjectField>();
                objectField.pickingMode = PickingMode.Ignore;
                
                var label = objectField.Q<Label>();
                label.pickingMode = PickingMode.Ignore;

                Build(x.changedProperty, depth);
            });

            Add(foldout);
            Add(field);

            Build(property, depth);
        }

        readonly Foldout foldout;
        readonly PropertyField field;
        bool isNull;

        public bool IsObjectNull => isNull;

        public string Label
        {
            get
            {
                if (isNull) return field.Q<Label>().text;
                else return foldout.text;
            }
            set
            {
                if (isNull) field.Q<Label>().text = value;
                else foldout.text = value;
            }
        }

        void Build(SerializedProperty property, int depth)
        {
            foldout.Clear();
            var toggle = foldout.Q<Toggle>();

            isNull = property.objectReferenceValue == null;
            toggle.style.display = isNull ? DisplayStyle.None : DisplayStyle.Flex;
            if (!isNull)
            {
                foldout.Add(new VisualElement() { style = { height = EditorGUIUtility.standardVerticalSpacing } });
                var so = new SerializedObject(property.objectReferenceValue);
                InspectorHelper.BuildElements(so, foldout, so.targetObject, name => so.FindProperty(name), depth);
                this.Bind(so);
            }
            else
            {
                this.Unbind();
            }
        }
    }
}