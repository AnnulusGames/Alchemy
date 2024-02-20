# カスタム属性を作成する

`AlchemyAttributeDrawer`を使用することで、Alchemy上で動作する独自の属性を作成することが可能です。ここでは例として`HelpBoxAttribute`とそのDrawerの実装を示します。

まずは、フィールドやプロパティに追加する属性を定義します。

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

次に、定義した属性に対応するDrawerを作成します。Drawerを定義したcsファイルはEditorフォルダ以下に配置する必要があります。

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

`OnCreateElement()`メソッドを実装することで、対象のメンバーに対応したVisualElementを作成した際に処理を追加することができます。描画処理を上書きする通常の`PropertyDrawer`とは異なり、こちらはVisual Elementの作成後に後処理を追加する動作であることに注意してください。この仕組みによってAlchemyは複数のDrawerを組み合わせることを可能にしています。

また、定義したDrawerには`CustomAttributeDrawer`属性を追加し、引数に定義した属性の型を追加する必要があります。この属性をもとにAlchemyは要素の描画に必要なDrawerの検索を行います。