using System;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    public abstract class AlchemyGroupDrawer
    {
        public abstract VisualElement CreateRootElement(string label);
        public virtual VisualElement GetGroupElement(Attribute attribute) => null;

        public string UniqueId => _uniqueId;
        string _uniqueId;

        internal void SetUniqueId(string id)
        {
            this._uniqueId = id;
        }
    }
}