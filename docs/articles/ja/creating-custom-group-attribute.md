# カスタムグループ属性を作成する

`AlchemyGroupDrawer`を使用することで、フィールドをグループ化するための独自の属性を作成することが可能です。ここでは例として`FoldoutGroupAttribute`のそのDrawerの実装を示します。(説明のため実際に使用されている実装から省略した部分があります。)

まずは、グループの定義に使用する属性を定義します。この属性は`PropertyGroupAttribute`を継承する必要があります。

```cs
using Alchemy.Inspector;

public sealed class FoldoutGroupAttribute : PropertyGroupAttribute
{
    public FoldoutGroupAttribute() : base() { }
    public FoldoutGroupAttribute(string groupPath) : base(groupPath) { }
}
```

次に、定義した属性に対応するDrawerを作成します。Drawerを定義したcsファイルはEditorフォルダ以下に配置する必要があります。

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

`CreateRootElement(string label)`メソッドを実装することで、各グループの親要素となるVisualElementの作成を行います。

また、定義したDrawerには`CustomGroupDrawer`属性を追加し、引数に定義した属性の型を追加する必要があります。この属性をもとにAlchemyはグループの描画に必要なDrawerの検索を行います。

