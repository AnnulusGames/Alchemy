# Creating Custom Group Attributes

By using `AlchemyGroupDrawer`, it is possible to create custom attributes for grouping fields. Here is an example demonstrating the implementation of `FoldoutGroupAttribute` and its corresponding drawer. (Some parts of the actual implementation have been omitted for the sake of explanation.)

First, define the attribute to be used for defining groups. This attribute must inherit from `PropertyGroupAttribute`.

```cs
using Alchemy.Inspector;

public sealed class FoldoutGroupAttribute : PropertyGroupAttribute
{
    public FoldoutGroupAttribute() : base() { }
    public FoldoutGroupAttribute(string groupPath) : base(groupPath) { }
}
```

Next, create the drawer corresponding to the defined attribute. Drawer scripts should be placed within the Editor folder.

```cs
using UnityEngine.UIElements;
using Alchemy.Editor;

[CustomGroupDrawer(typeof(FoldoutGroupAttribute))]
public sealed class FoldoutGroupDrawer : AlchemyGroupDrawer
{
    public override VisualElement CreateRootElement(string label)
    {
        var foldout = new Foldout()
        {
            style = {
                width = Length.Percent(100f)
            },
            text = label
        };

        return foldout;
    }
}
```

Implement the `CreateRootElement(string label)` method to create the root VisualElement for each group. Additionally, make sure to add the `CustomGroupDrawer` attribute to the defined drawer, with the type of the defined attribute as an argument. Alchemy uses this attribute to search for the necessary drawers for group rendering.