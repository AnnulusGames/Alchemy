using UnityEditor;
using UnityEngine;
using Alchemy.Hierarchy;

namespace Alchemy.Editor
{
    public sealed class HierarchyButtonDrawer : HierarchyDrawer
    {
        public override void OnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (gameObject == null) return;
            if (gameObject.TryGetComponent<HierarchyObject>(out _)) return;

            if (AlchemySettings.GetOrCreateSettings().ShowHierarchyToggles)
            {
                var pos = selectionRect;
                pos.x = pos.xMax - 8f;
                pos.width = 16f;

                var active = GUI.Toggle(pos, gameObject.activeSelf, string.Empty);
                if (active != gameObject.activeSelf)
                {
                    Undo.RecordObject(gameObject, $"{(active ? "Activate" : "Deactivate")} GameObject '{gameObject.name}'");
                    gameObject.SetActive(active);
                    EditorUtility.SetDirty(gameObject);
                }
            }
        }
    }

}