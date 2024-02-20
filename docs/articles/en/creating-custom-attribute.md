# Creating Custom Attributes

By using `AlchemyAttributeDrawer`, it is possible to create custom attributes that work within Alchemy. Here is an example demonstrating the implementation of `HelpBoxAttribute` and its corresponding drawer.

First, define the attribute to be added to fields or properties.

```cs
using System;
using UnityEngine.UIElements;

public sealed class HelpBoxAttribute : Attribute
{
    public HelpBoxAttribute(string message, HelpBoxMessageType messageType = HelpBoxMessageType.Info)
    {
        Message = message;
        MessageType = messageType;
    }

    public string Message { get; }
    public HelpBoxMessageType MessageType { get; }
}
```

Next, create the drawer corresponding to the defined attribute. Drawer scripts should be placed within the Editor folder.

```cs
using UnityEngine.UIElements;
using Alchemy.Editor;

[CustomAttributeDrawer(typeof(HelpBoxAttribute))]
public sealed class HelpBoxDrawer : AlchemyAttributeDrawer
{
    HelpBox helpBox;

    public override void OnCreateElement()
    {
        var att = (HelpBoxAttribute)Attribute;
        helpBox = new HelpBox(att.Message, att.MessageType);

        var parent = TargetElement.parent;
        parent.Insert(parent.IndexOf(TargetElement), helpBox);
    }
}
```

Implement the `OnCreateElement()` method to add processing when creating the corresponding VisualElement for the member. Unlike regular `PropertyDrawer`s that override the drawing process, here we're adding post-processing after the creation of the Visual Element. This mechanism allows Alchemy to combine multiple drawers.

Additionally, make sure to add the `CustomAttributeDrawer` attribute to the defined drawer, with the type of the defined attribute as an argument. Alchemy uses this attribute to search for the necessary drawers for element rendering.
