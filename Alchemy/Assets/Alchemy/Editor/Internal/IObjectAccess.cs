using UnityEditor;

namespace Alchemy.Editor
{
    public interface IObjectAccess
    {
        public object Target { get; set; }

        public static IObjectAccess Create(SerializedProperty property)
        {
            var value = property.GetValue<object>();
            if (value.GetType().IsValueType)
            {
                return new SerializedStructAccess(property);
            }
            else
            {
                return new IdentityAccess(value);
            }
        }
    }

    public class IdentityAccess : IObjectAccess
    {
        public object Target { get; set; }

        public IdentityAccess(object target)
        {
            Target = target;
        }
    }

    public class SerializedStructAccess : IObjectAccess
    {
        public SerializedStructAccess(SerializedProperty property)
        {
            this.property = property;
        }

        readonly SerializedProperty property;

        public object Target
        {
            get => property.GetValue<object>();
            set => property.SetValue<object>(value);
        }
    }
}