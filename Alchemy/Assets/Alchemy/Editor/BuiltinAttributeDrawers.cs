using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using Alchemy.Inspector;
using Alchemy.Editor.Elements;

namespace Alchemy.Editor.Drawers
{
    [CustomAttributeDrawer(typeof(ReadOnlyAttribute))]
    public sealed class ReadOnlyDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
        {
            Element.SetEnabled(false);
        }
    }

    [CustomAttributeDrawer(typeof(IndentAttribute))]
    public sealed class IndentDrawer : AlchemyAttributeDrawer
    {
        const float IndentPadding = 15f;

        public override void OnCreateElement()
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

    [CustomAttributeDrawer(typeof(HideInPlayModeAttribute))]
    public sealed class HideInPlayModeDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
        {
            Element.style.display = Application.isPlaying ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomAttributeDrawer(typeof(HideInEditModeAttribute))]
    public sealed class HideInEditModeDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
        {
            Element.style.display = !Application.isPlaying ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomAttributeDrawer(typeof(DisableInPlayModeAttribute))]
    public sealed class DisableInPlayModeDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
        {
            if (Application.isPlaying) Element.SetEnabled(false);
        }
    }

    [CustomAttributeDrawer(typeof(DisableInEditModeAttribute))]
    public sealed class DisableInEditModeDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
        {
            if (!Application.isPlaying) Element.SetEnabled(false);
        }
    }

    [CustomAttributeDrawer(typeof(HideLabelAttribute))]
    public sealed class HideLabelDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
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

    [CustomAttributeDrawer(typeof(LabelTextAttribute))]
    public sealed class LabelTextDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
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

    [CustomAttributeDrawer(typeof(HideIfAttribute))]
    public sealed class HideIfDrawer : TrackSerializedObjectAttributeDrawer
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((HideIfAttribute)Attribute).Condition);
            Element.style.display = condition ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomAttributeDrawer(typeof(ShowIfAttribute))]
    public sealed class ShowIfDrawer : TrackSerializedObjectAttributeDrawer
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((ShowIfAttribute)Attribute).Condition);
            Element.style.display = !condition ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomAttributeDrawer(typeof(DisableIfAttribute))]
    public sealed class DisableIfDrawer : TrackSerializedObjectAttributeDrawer
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((DisableIfAttribute)Attribute).Condition);
            Element.SetEnabled(!condition);
        }
    }

    [CustomAttributeDrawer(typeof(EnableIfAttribute))]
    public sealed class EnableIfDrawer : TrackSerializedObjectAttributeDrawer
    {
        protected override void OnInspectorChanged()
        {
            var condition = ReflectionHelper.GetValueBool(Target, ((EnableIfAttribute)Attribute).Condition);
            Element.SetEnabled(condition);
        }
    }

    [CustomAttributeDrawer(typeof(RequiredAttribute))]
    public sealed class RequiredDrawer : TrackSerializedObjectAttributeDrawer
    {
        HelpBox helpBox;

        public override void OnCreateElement()
        {
            if (SerializedProperty.propertyType != SerializedPropertyType.ObjectReference) return;

            var message = ((RequiredAttribute)Attribute).Message ?? ObjectNames.NicifyVariableName(SerializedProperty.displayName) + " is required.";
            helpBox = new HelpBox(message, HelpBoxMessageType.Error);

            var parent = Element.parent;
            parent.Insert(parent.IndexOf(Element), helpBox);

            base.OnCreateElement();
        }

        protected override void OnInspectorChanged()
        {
            helpBox.style.display = SerializedProperty.objectReferenceValue != null ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomAttributeDrawer(typeof(ValidateInputAttribute))]
    public sealed class ValidateInputDrawer : TrackSerializedObjectAttributeDrawer
    {
        HelpBox helpBox;

        public override void OnCreateElement()
        {
            var message = ((ValidateInputAttribute)Attribute).Message ?? ObjectNames.NicifyVariableName(SerializedProperty.displayName) + " is not valid.";
            helpBox = new HelpBox(message, HelpBoxMessageType.Error);

            var parent = Element.parent;
            parent.Insert(parent.IndexOf(Element), helpBox);

            base.OnCreateElement();
        }

        protected override void OnInspectorChanged()
        {
            var result = ReflectionHelper.Invoke(Target, ((ValidateInputAttribute)Attribute).Condition, SerializedProperty.GetValue<object>());
            helpBox.style.display = result is bool flag && flag ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }

    [CustomAttributeDrawer(typeof(HelpBoxAttribute))]
    public sealed class HelpBoxDrawer : AlchemyAttributeDrawer
    {
        HelpBox helpBox;

        public override void OnCreateElement()
        {
            var att = (HelpBoxAttribute)Attribute;
            helpBox = new HelpBox(att.Message, att.MessageType);

            var parent = Element.parent;
            parent.Insert(parent.IndexOf(Element), helpBox);
        }
    }

    [CustomAttributeDrawer(typeof(HorizontalLineAttribute))]
    public sealed class HorizontalLineDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
        {
            var att = (HorizontalLineAttribute)Attribute;
            var parent = Element.parent;
            var lineColor = att.Color == default ? GUIHelper.LineColor : att.Color;
            var line = GUIHelper.CreateLine(lineColor, EditorGUIUtility.standardVerticalSpacing * 4f);
            parent.Insert(parent.IndexOf(Element), line);
        }
    }

    [CustomAttributeDrawer(typeof(TitleAttribute))]
    public sealed class TitleDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
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

    [CustomAttributeDrawer(typeof(BlockquoteAttribute))]
    public sealed class BlockquoteDrawer : AlchemyAttributeDrawer
    {
        public BlockquoteDrawer()
        {
            textStyle = EditorStyles.label;
            textStyle.wordWrap = true;
        }

        readonly GUIStyle textStyle;

        public override void OnCreateElement()
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