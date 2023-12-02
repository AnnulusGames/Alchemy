using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;
using Alchemy.Editor.Internal;
using UnityEditor.UIElements;
#if ALCHEMY_SUPPORT_SERIALIZATION
using Alchemy.Serialization;
#endif

namespace Alchemy.Editor
{
    using Editor = UnityEditor.Editor;

    /// <summary>
    /// Editor base class for Inspector drawing in Alchemy
    /// </summary>
    public abstract class AlchemyEditor : Editor
    {
        const string ScriptFieldName = "m_Script";
#if ALCHEMY_SUPPORT_SERIALIZATION
        const string AlchemySerializationWarning = "In the current version, fields with the [AlchemySerializedField] attribute do not support editing multiple objects.";
#endif

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var targetType = target.GetType();

            if (targetType.HasCustomAttribute<DisableAlchemyEditorAttribute>())
            {
                // Create default inspector
                InspectorElement.FillDefaultInspector(root, serializedObject, this);
                return root;
            }

#if ALCHEMY_SUPPORT_SERIALIZATION
            if (targetType.HasCustomAttribute<AlchemySerializeAttribute>() && targets.Length > 1)
            {
                root.Add(new HelpBox(AlchemySerializationWarning, HelpBoxMessageType.Error));
            }
#endif

            // Add script field
            if (targetType.GetCustomAttribute<HideScriptFieldAttribute>() == null)
            {
                var scriptField = new PropertyField(serializedObject.FindProperty(ScriptFieldName));
                scriptField.SetEnabled(false);
                root.Add(scriptField);
                root.Add(new VisualElement()
                {
                    style = { height = EditorGUIUtility.standardVerticalSpacing * 0.5f }
                });
            }

            // Add elements
            InspectorHelper.BuildElements(serializedObject, root, target, name => serializedObject.FindProperty(name), 0);

            return root;
        }
    }

#if !ALCHEMY_DISABLE_DEFAULT_EDITOR

    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true, isFallback = true)]
    [CanEditMultipleObjects]
    internal sealed class MonoBehaviourEditor : AlchemyEditor { }

    [CustomEditor(typeof(ScriptableObject), editorForChildClasses: true, isFallback = true)]
    [CanEditMultipleObjects]
    internal sealed class ScriptableObjectEditor : AlchemyEditor { }
#endif

}