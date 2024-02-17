namespace Alchemy.Inspector
{
    public sealed class GroupAttribute : PropertyGroupAttribute
    {
        public GroupAttribute() : base() { }
        public GroupAttribute(string groupPath) : base(groupPath) { }
    }

    public sealed class BoxGroupAttribute : PropertyGroupAttribute
    {
        public BoxGroupAttribute() : base() { }
        public BoxGroupAttribute(string groupPath) : base(groupPath) { }
    }

    public sealed class TabGroupAttribute : PropertyGroupAttribute
    {
        public TabGroupAttribute(string groupPath, string tabName) : base(groupPath)
        {
            TabName = tabName;
        }

        public string TabName { get; }
    }

    public sealed class FoldoutGroupAttribute : PropertyGroupAttribute
    {
        public FoldoutGroupAttribute() : base() { }
        public FoldoutGroupAttribute(string groupPath) : base(groupPath) { }
    }

    public sealed class HorizontalGroupAttribute : PropertyGroupAttribute
    {
        public HorizontalGroupAttribute(string groupPath) : base(groupPath) { }
    }

}