using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Assertions;
using Alchemy.Inspector;
using System;

namespace Alchemy.Editor
{
    internal static class GUIHelper
    {
        public static Color LineColor => EditorGUIUtility.isProSkin ? new(0.4f, 0.4f, 0.4f) : new(0.6f, 0.6f, 0.6f);
        public static Color SubtitleColor => new(0.5f, 0.5f, 0.5f);
        public static Color TextColor => EditorGUIUtility.isProSkin ? new(0.725f, 0.725f, 0.725f) : new(0.141f, 0.141f, 0.141f);

        public static float CalculateFieldWidth(VisualElement element, VisualElement root)
        {
            var labelWidth = CalculateLabelWidth(element, root);
            return root.resolvedStyle.width - labelWidth - 27f;
        }

        public static float CalculateLabelWidth(VisualElement element, VisualElement root)
        {
            // This code is a partial modification of the Label width calculation method actually used inside PropertyField.
            var num = root.resolvedStyle.paddingLeft;
            var num2 = 37f;
            var num3 = 123f;
            var num4 = element.GetFirstAncestorOfType<Foldout>() == null ? 0f : 15f;

            var width = root.resolvedStyle.width;
            var a = width * 0.45f - num2 - num - num4;
            var b = Mathf.Max(num3 - num - num4, 0f);

            return Mathf.Max(a, b) + 12f;
        }

        public static ListView CreateDefaultListView(string label)
        {
            return new ListView()
            {
                reorderable = true,
                reorderMode = ListViewReorderMode.Animated,
                showBorder = true,
                showFoldoutHeader = true,
                headerTitle = label,
                showAddRemoveFooter = true,
                fixedItemHeight = 20f,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                showAlternatingRowBackgrounds = AlternatingRowBackground.None,
            };
        }

        public static PropertyField CreateObjectPropertyField(SerializedProperty property, Type type)
        {
            Assert.IsTrue(property.propertyType == SerializedPropertyType.ObjectReference);

            var fieldInfo = property.GetFieldInfo();
            var isAssetsOnly = fieldInfo.HasCustomAttribute<AssetsOnlyAttribute>();

            var propertyField = new PropertyField(property);
            propertyField.RegisterValueChangeCallback(x =>
            {
                var objectField = propertyField.Q<ObjectField>();
                objectField.objectType = type;
                objectField.allowSceneObjects = !isAssetsOnly;
            });

            return propertyField;
        }

        public static void ScheduleAdjustLabelWidth(VisualElement element)
        {
            void Adjust(VisualElement visualElement)
            {
                var label = element.Q<Label>();
                if (label == null) return;
                label.style.minWidth = 0f;
                label.style.width = CalculateLabelWidth(element, visualElement);
            }

            // Adjust label width
            element.schedule.Execute(() =>
            {
                var visualTree = element.panel.visualTree;
                visualTree.RegisterCallback<GeometryChangedEvent>(x => Adjust(visualTree));
                Adjust(visualTree);
            });
        }

        public static IMGUIContainer CreateLine(Color color, float height)
        {
            return new IMGUIContainer(() =>
            {
                var rect = EditorGUILayout.GetControlRect(false, height);
                rect.xMin += 3f;
                rect.y += rect.height * 0.5f;
                rect.height = 1f;
                EditorGUI.DrawRect(rect, color);
            });

        }
    }
}