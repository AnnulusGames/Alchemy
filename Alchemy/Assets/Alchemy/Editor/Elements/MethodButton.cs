using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    public sealed class MethodButton : VisualElement
    {
        const string ButtonLabelText = "Invoke";

        public MethodButton(IObjectAccess objectAccess, MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();

            // Create parameterless button
            if (parameters.Length == 0)
            {
                button = new Button(() =>
                {
                    var target = objectAccess.Target;
                    methodInfo.Invoke(target, null);
                    objectAccess.Target = target;
                })
                {
                    text = methodInfo.Name
                };
                Add(button);
                return;
            }

            var parameterObjects = new object[parameters.Length];

            var box = new HelpBox();
            Add(box);

            foldout = new Foldout()
            {
                text = methodInfo.Name,
                value = false,
                style = {
                    flexGrow = 1f
                }
            };
            InternalAPIHelper.SetAcceptClicksIfDisabled(
                InternalAPIHelper.GetClickable(foldout.Q<Toggle>()), true
            );

            button = new Button(() =>    {
                var target = objectAccess.Target;
                methodInfo.Invoke(target, parameterObjects);
                objectAccess.Target = target;
            })
            {
                text = ButtonLabelText,
                style = {
                    position = Position.Absolute,
                    right = 1f,
                    top = 1.5f,
                    width = 100f
                }
            };

            box.Add(new VisualElement() { style = { width = 12f } });
            box.Add(foldout);
            box.Add(button);

            for (int i = 0; i < parameters.Length; i++)
            {
                var index = i;
                var parameter = parameters[index];
                parameterObjects[index] = TypeHelper.CreateDefaultInstance(parameter.ParameterType);
                var element = new GenericField(new IdentityAccess( parameterObjects[index]), parameter.ParameterType, ObjectNames.NicifyVariableName(parameter.Name), 0);
                element.OnValueChanged += x => parameterObjects[index] = x;
                element.style.paddingRight = 4f;
                foldout.Add(element);
            }
        }

        readonly Foldout foldout;
        readonly Button button;
    }
}