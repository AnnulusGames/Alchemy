using System.Linq;
using Alchemy.Editor.Internal;
using UnityEditor;
using UnityEngine;

namespace Alchemy.Editor
{
    public abstract class HierarchyDrawer
    {
        public abstract void OnGUI(int instanceID, Rect selectionRect);

        protected static Rect GetBackgroundRect(Rect selectionRect)
        {
            return selectionRect.AddXMin(-28f).AddXMax(20f);
        }

        protected static void DrawBackground(int instanceID, Rect selectionRect)
        {
            var backgroundRect = GetBackgroundRect(selectionRect);

            Color backgroundColor;
            var e = Event.current;
            var isHover = backgroundRect.Contains(e.mousePosition);

            if (Selection.Contains(instanceID))
            {
                backgroundColor = EditorColors.HighlightBackground;
            }
            else if (isHover)
            {
                backgroundColor = EditorColors.HighlightBackgroundInactive;
            }
            else
            {
                backgroundColor = EditorColors.WindowBackground;
            }

            EditorGUI.DrawRect(backgroundRect, backgroundColor);
        }
    }
}