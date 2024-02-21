using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Visual Element that creates a suitable input Field from object type
    /// </summary>
    public sealed class GenericField : VisualElement
    {
        const string CreateButtonText = "Create...";
        // public GenericField(object  obj, Type type, string label, int depth, bool isDelayed = false)
        // {
        //     Build(new IdentityAccess(obj), type, label, depth, isDelayed);
        //     GUIHelper.ScheduleAdjustLabelWidth(this);
        // }
        public GenericField(IObjectAccess  objectAccess, Type type, string label, int depth, bool isDelayed = false)
        {
            Build(objectAccess, type, label, depth, isDelayed);
            GUIHelper.ScheduleAdjustLabelWidth(this);
        }

        void Build(IObjectAccess  objectAccess, Type type, string label, int depth, bool isDelayed)
        {
            if (depth > InspectorHelper.MaxDepth) return;
            Clear();
            var obj = objectAccess.Target;
            // Add [Create...] button
            if (obj == null && !typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                var nullLabelElement = new VisualElement()
                {
                    style = {
                        width = Length.Percent(100f),
                        height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                        paddingLeft = 3f,
                        flexDirection = FlexDirection.Row
                    }
                };

                nullLabelElement.Add(new Label(label + " (Null)")
                {
                    style = {
                        flexGrow = 1f,
                        unityTextAlign = TextAnchor.MiddleLeft
                    }
                });

                // TODO: support polymorphism
                if (type == typeof(string))
                {
                    nullLabelElement.Add(new Button(() =>
                    {
                        var instance = "";
                        Build(new IdentityAccess(instance), type, label, depth, isDelayed);
                        OnValueChanged?.Invoke(instance);
                    })
                    {
                        text = CreateButtonText
                    });
                }
                else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) // nullable
                {
                    nullLabelElement.Add(new Button(() =>
                    {
                        var instance = Activator.CreateInstance(type, Activator.CreateInstance(type.GenericTypeArguments[0]));
                        Build(new IdentityAccess(instance), type, label, depth, isDelayed);
                        OnValueChanged?.Invoke(instance);
                    })
                    {
                        text = CreateButtonText
                    });
                }
                else if (TypeHelper.HasDefaultConstructor(type))
                {
                    nullLabelElement.Add(new Button(() =>
                    {
                        var instance = TypeHelper.CreateDefaultInstance(type);
                        Build(new IdentityAccess(instance), type, label, depth, isDelayed);
                        OnValueChanged?.Invoke(instance);
                    })
                    {
                        text = CreateButtonText
                    });
                }

                Add(nullLabelElement);

                return;
            }

            this.isDelayed = isDelayed;

            if (type == typeof(bool))
            {
                AddField(new Toggle(label), (bool)obj);
            }
            else if (type == typeof(int))
            {
                AddField(new IntegerField(label), (int)obj);
            }
            else if (type == typeof(uint))
            {
                AddField(new UnsignedIntegerField(label), (uint)obj);
            }
            else if (type == typeof(long))
            {
                AddField(new LongField(label), (long)obj);
            }
            else if (type == typeof(ulong))
            {
                AddField(new UnsignedLongField(label), (ulong)obj);
            }
            else if (type == typeof(float))
            {
                AddField(new FloatField(label), (float)obj);
            }
            else if (type == typeof(double) || type == typeof(decimal))
            {
                AddField(new DoubleField(label), (double)obj);
            }
            else if (type == typeof(string))
            {
                AddField(new TextField(label), (string)obj);
            }
            else if (type == typeof(char))
            {
                var charField = new TextField(label, 1, false, false, default) { value = obj.ToString() };
                charField.RegisterValueChangedCallback(x => OnValueChanged?.Invoke(x.newValue[0]));
                Add(charField);
            }
            else if (type.IsEnum)
            {
                var value = (Enum)obj;
                if (value.GetType().HasCustomAttribute<FlagsAttribute>()) AddField(new EnumFlagsField(label, value), value);
                else AddField(new EnumField(label, value), value);
            }
            else if (type == typeof(Vector2))
            {
                AddField(new Vector2Field(label), (Vector2)obj);
            }
            else if (type == typeof(Vector2Int))
            {
                AddField(new Vector2IntField(label), (Vector2Int)obj);
            }
            else if (type == typeof(Vector3))
            {
                AddField(new Vector3Field(label), (Vector3)obj);
            }
            else if (type == typeof(Vector3Int))
            {
                AddField(new Vector3IntField(label), (Vector3Int)obj);
            }
            else if (type == typeof(Vector4))
            {
                AddField(new Vector4Field(label), (Vector4)obj);
            }
            else if (type == typeof(Rect))
            {
                AddField(new RectField(label), (Rect)obj);
            }
            else if (type == typeof(RectInt))
            {
                AddField(new RectIntField(label), (RectInt)obj);
            }
            else if (type == typeof(Bounds))
            {
                AddField(new BoundsField(label), (Bounds)obj);
            }
            else if (type == typeof(BoundsInt))
            {
                AddField(new BoundsIntField(label), (BoundsInt)obj);
            }
            else if (type == typeof(Color))
            {
                AddField(new ColorField(label), (Color)obj);
            }
            else if (type == typeof(Gradient))
            {
                AddField(new GradientField(label), (Gradient)obj);
            }
            else if (type == typeof(Hash128))
            {
                AddField(new Hash128Field(label), (Hash128)obj);
            }
            else if (type == typeof(AnimationCurve))
            {
                AddField(new CurveField(label), (AnimationCurve)obj);
            }
            else if (type == typeof(Gradient))
            {
                AddField(new GradientField(label), (Gradient)obj);
            }
            else if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                AddField(new ObjectField(label) { objectType = type }, (UnityEngine.Object)obj);
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(HashSet<>))
            {
                var field = new HashSetField(obj, label, depth + 1);
                field.OnValueChanged += x => OnValueChanged?.Invoke(x);

                Add(field);
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var field = new DictionaryField(obj, label, depth + 1);
                field.OnValueChanged += x => OnValueChanged?.Invoke(x);
                Add(field);
            }
            else if (typeof(IList).IsAssignableFrom(type))
            {
                var field = new ListField((IList)obj, label, depth + 1);
                field.OnValueChanged += x => OnValueChanged?.Invoke(x);
                Add(field);
            }
            else
            {
                var field = new ClassField(objectAccess, type, label, depth + 1);
                field.OnValueChanged += x => OnValueChanged?.Invoke(x);
                Add(field);
            }
        }

        public event Action<object> OnValueChanged;
        bool isDelayed;
        bool changed;

        void AddField<T>(BaseField<T> control, T value)
        {
            control.value = value;
            if (isDelayed && control is not ObjectField) // ignore ObjectField
            {
                control.RegisterValueChangedCallback(x => changed = true);
                control.RegisterCallback<FocusOutEvent>(x =>
                {
                    if (changed)
                    {
                        OnValueChanged?.Invoke(control.value);
                        changed = false;
                    }
                });
            }
            else
            {
                control.RegisterValueChangedCallback(x => OnValueChanged?.Invoke(x.newValue));
            }
            Add(control);
        }
    }
}