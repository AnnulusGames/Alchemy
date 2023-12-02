using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;
using Alchemy.Editor.Internal;
using Alchemy.Editor.Elements;
using UnityEditor;

namespace Alchemy.Editor.Processors
{
    [CustomPropertyProcessor(typeof(ReadOnlyAttribute))]
    public sealed class ReadOnlyProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            Element.SetEnabled(false);
        }
    }

    [CustomPropertyProcessor(typeof(IndentAttribute))]
    public sealed class IndentProcessor : PropertyProcessor
    {
        const float IndentPadding = 15f;

        public override void Execute()
        {
            Element.RegisterCallback<GeometryChangedEvent>(x => AddPadding());
        }

        void AddPadding()
        {
            var label = Element.Q<Label>();
            if (label == null) return;
            label.style.paddingLeft = ((IndentAttribute)Attribute).indent * IndentPadding;
        }
    }

    [CustomPropertyProcessor(typeof(HideInPlayModeAttribute))]
    public sealed class HideInPlayModeProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            Element.style.display = Application.isPlaying ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomPropertyProcessor(typeof(HideInEditModeAttribute))]
    public sealed class HideInEditModeProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            Element.style.display = !Application.isPlaying ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomPropertyProcessor(typeof(DisableInPlayModeAttribute))]
    public sealed class DisableInPlayModeProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            if (Application.isPlaying) Element.SetEnabled(false);
        }
    }

    [CustomPropertyProcessor(typeof(DisableInEditModeAttribute))]
    public sealed class DisableInEditModeProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            if (!Application.isPlaying) Element.SetEnabled(false);
        }
    }

    [CustomPropertyProcessor(typeof(HideLabelAttribute))]
    public sealed class HideLabelProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            if (Element is AlchemyPropertyField field)
            {
                field.Label = string.Empty;
                return;
            }

            var labelElement = Element.Q<Label>();
            if (labelElement == null) return;
            labelElement.text = string.Empty;
        }
    }

    [CustomPropertyProcessor(typeof(LabelTextAttribute))]
    public sealed class LabelTextProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            var labelTextAttribute = (LabelTextAttribute)Attribute;

            switch (Element)
            {
                case AlchemyPropertyField alchemyPropertyField:
                    alchemyPropertyField.Label = labelTextAttribute.Text;
                    break;
                case Button button:
                    button.text = labelTextAttribute.Text;
                    break;
                default:
                    var labelElement = Element.Q<Label>();
                    if (labelElement == null) return;
                    labelElement.text = labelElement.text;
                    break;
            }
        }
    }

    [CustomPropertyProcessor(typeof(HideIfAttribute))]
    public sealed class HideIfProcessor : TrackSerializedObjectPropertyProcessor
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((HideIfAttribute)Attribute).Condition);
            Element.style.display = condition ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomPropertyProcessor(typeof(ShowIfAttribute))]
    public sealed class ShowIfProcessor : TrackSerializedObjectPropertyProcessor
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((ShowIfAttribute)Attribute).Condition);
            Element.style.display = !condition ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomPropertyProcessor(typeof(DisableIfAttribute))]
    public sealed class DisableIfProcessor : TrackSerializedObjectPropertyProcessor
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((DisableIfAttribute)Attribute).Condition);
            Element.SetEnabled(!condition);
        }
    }

    [CustomPropertyProcessor(typeof(EnableIfAttribute))]
    public sealed class EnableIfProcessor : TrackSerializedObjectPropertyProcessor
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((EnableIfAttribute)Attribute).Condition);
            Element.SetEnabled(condition);
        }
    }

    [CustomPropertyProcessor(typeof(RequiredAttribute))]
    public sealed class RequiredProcessor : TrackSerializedObjectPropertyProcessor
    {
        HelpBox helpBox;

        public override void Execute()
        {
            if (SerializedProperty.propertyType != SerializedPropertyType.ObjectReference) return;

            var message = ((RequiredAttribute)Attribute).Message ?? ObjectNames.NicifyVariableName(SerializedProperty.displayName) + " is required.";
            helpBox = new HelpBox(message, HelpBoxMessageType.Error);

            var parent = Element.parent;
            parent.Insert(parent.IndexOf(Element), helpBox);

            base.Execute();
        }

        protected override void OnInspectorChanged()
        {
            helpBox.style.display = SerializedProperty.objectReferenceValue != null ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomPropertyProcessor(typeof(ValidateInputAttribute))]
    public sealed class ValidateInputProcessor : TrackSerializedObjectPropertyProcessor
    {
        HelpBox helpBox;

        public override void Execute()
        {
            var message = ((ValidateInputAttribute)Attribute).Message ?? ObjectNames.NicifyVariableName(SerializedProperty.displayName) + " is not valid.";
            helpBox = new HelpBox(message, HelpBoxMessageType.Error);

            var parent = Element.parent;
            parent.Insert(parent.IndexOf(Element), helpBox);

            base.Execute();
        }

        protected override void OnInspectorChanged()
        {
            var result = ReflectionHelper.Invoke(Target, ((ValidateInputAttribute)Attribute).Condition, SerializedProperty.GetValue<object>());
            helpBox.style.display = result is bool flag && flag ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomPropertyProcessor(typeof(HelpBoxAttribute))]
    public sealed class HelpBoxProcessor : PropertyProcessor
    {
        HelpBox helpBox;

        public override void Execute()
        {
            var att = (HelpBoxAttribute)Attribute;
            helpBox = new HelpBox(att.Message, att.MessageType);

            var parent = Element.parent;
            parent.Insert(parent.IndexOf(Element), helpBox);
        }
    }

    [CustomPropertyProcessor(typeof(HorizontalLineAttribute))]
    public sealed class HorizontalLineProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            var att = (HorizontalLineAttribute)Attribute;
            var parent = Element.parent;
            var line = GUIHelper.CreateLine(att.Color, EditorGUIUtility.standardVerticalSpacing * 4f);
            parent.Insert(parent.IndexOf(Element), line);
        }
    }

    [CustomPropertyProcessor(typeof(TitleAttribute))]
    public sealed class TitleProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            var att = (TitleAttribute)Attribute;
            var parent = Element.parent;

            var title = new Label(att.TitleText)
            {
                style = {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    paddingLeft = 3f,
                    marginTop = 4f,
                    marginBottom = -2f
                }
            };
            parent.Insert(parent.IndexOf(Element), title);

            if (att.SubitleText != null)
            {
                var subtitle = new Label(att.SubitleText)
                {
                    style = {
                        fontSize = 10f,
                        paddingLeft = 4.5f,
                        marginTop = 1.5f,
                        color = GUIHelper.SubtitleColor,
                        unityTextAlign = TextAnchor.MiddleLeft
                    }
                };
                parent.Insert(parent.IndexOf(Element), subtitle);
            }

            var line = GUIHelper.CreateLine(GUIHelper.LineColor, EditorGUIUtility.standardVerticalSpacing * 3f);
            parent.Insert(parent.IndexOf(Element), line);
        }
    }

    [CustomPropertyProcessor(typeof(BlockquoteAttribute))]
    public sealed class BlockquoteProcessor : PropertyProcessor
    {
        public BlockquoteProcessor()
        {
            textStyle = EditorStyles.label;
            textStyle.wordWrap = true;
        }

        readonly GUIStyle textStyle;

        public override void Execute()
        {
            var att = (BlockquoteAttribute)Attribute;
            var blockquote = new IMGUIContainer(() =>
            {
                var width = EditorGUIUtility.currentViewWidth;
                var labelContent = new GUIContent(att.Text);
                var labelHeight = textStyle.CalcHeight(labelContent, width - 3f);
                var position = EditorGUILayout.GetControlRect(false, labelHeight + EditorGUIUtility.standardVerticalSpacing * 2f);

                var blockRect = position;
                var backgroundColor = GUIHelper.TextColor;
                backgroundColor.a = 0.06f;
                EditorGUI.DrawRect(blockRect, backgroundColor);
                blockRect.x = position.xMin;
                blockRect.width = 3;
                EditorGUI.DrawRect(blockRect, GUIHelper.TextColor);

                var labelPosition = position;
                labelPosition.xMin += 7f;
                EditorGUI.LabelField(labelPosition, labelContent, textStyle);
            });

            var parent = Element.parent;
            parent.Insert(parent.IndexOf(Element), blockquote);
        }
    }

}