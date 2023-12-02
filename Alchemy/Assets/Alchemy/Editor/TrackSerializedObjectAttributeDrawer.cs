using UnityEditor.UIElements;

namespace Alchemy.Editor.Drawers
{
    public abstract class TrackSerializedObjectAttributeDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
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