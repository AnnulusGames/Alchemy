using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using Alchemy.Editor.Internal;

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

            var listView = GUIHelper.CreateDefaultListView(ObjectNames.NicifyVariableName(property.displayName));
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

            listView.Q<Label>().style.unityFontStyleAndWeight = FontStyle.Bold;

            listView.BindProperty(property);
            Add(listView);
        }
    }
}