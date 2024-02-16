using UnityEditor;
using UnityEngine.UIElements;
using Alchemy.Hierarchy;
using UnityEditor.UIElements;

namespace Alchemy.Editor
{
    [CustomEditor(typeof(HierarchyObject), true)]
    [CanEditMultipleObjects]
    public sealed class HierarchyObjectEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var iterator = serializedObject.GetIterator();
            iterator.NextVisible(true);

            while (iterator.NextVisible(false))
            {
                root.Add(new PropertyField(iterator));
            }

            return root;
        }
    }
}