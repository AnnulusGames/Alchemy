using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using Alchemy.Inspector;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Visual Element that draws SerializedProperty of Array or List
    /// </summary>
    public sealed class PropertyListView : BindableElement
    {
        public PropertyListView(SerializedProperty property, int depth)
        {
            Assert.IsTrue(property.isArray);

            var settings = property.GetAttribute<ListViewSettingsAttribute>(true);

            var listView = GUIHelper.CreateListViewFromSettingsAttribute(settings);
            listView.headerTitle = ObjectNames.NicifyVariableName(property.displayName);
            listView.bindItem = (element, index) =>
            {
                var arrayElement = property.GetArrayElementAtIndex(index);
                var e = new AlchemyPropertyField(arrayElement, property.GetPropertyType(true), depth + 1, true);
                element.Add(e);
                element.Bind(arrayElement.serializedObject);
            };
            listView.unbindItem = (element, index) =>
            {
                element.Clear();
                element.Unbind();
            };

            var label = listView.Q<Label>();
            if (label != null) label.style.unityFontStyleAndWeight = FontStyle.Bold;

            listView.BindProperty(property);
            Add(listView);
        }
    }
}