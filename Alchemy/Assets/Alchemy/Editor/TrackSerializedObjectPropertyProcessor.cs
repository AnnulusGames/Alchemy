using UnityEditor.UIElements;

namespace Alchemy.Editor.Processors
{
    public abstract class TrackSerializedObjectPropertyProcessor : PropertyProcessor
    {
        public override void Execute()
        {
            Element.TrackSerializedObjectValue(SerializedObject, x =>
            {
                OnInspectorChanged();
            });

            OnInspectorChanged();
            Element.schedule.Execute(() => OnInspectorChanged());
        }

        protected abstract void OnInspectorChanged();
    }
}