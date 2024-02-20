# グループ属性

Alchemyでは、フィールドをグループ化する属性が提供されています。

```cs
using UnityEngine;
using Alchemy.Inspector;

public class GroupAttributesExample : MonoBehaviour
{
    [FoldoutGroup("Foldout")]
    public int a;

    [FoldoutGroup("Foldout")]
    public int b;

    [FoldoutGroup("Foldout")]
    public int c;

    [TabGroup("Tab", "Tab1")]
    public int x;

    [TabGroup("Tab", "Tab2")]
    public string y;

    [TabGroup("Tab", "Tab3")]
    public Vector3 z;
}
```

![img](../../images/img-group-1.png)

各グループはスラッシュで区切ることでネストできます。

```cs
using UnityEngine;
using Alchemy.Inspector;

public class GroupAttributesExample : MonoBehaviour
{
    [HorizontalGroup("Horizontal"), BoxGroup("Horizontal/Box1")]
    public float foo;

    [HorizontalGroup("Horizontal"), BoxGroup("Horizontal/Box1")]
    public Vector3 bar;

    [HorizontalGroup("Horizontal"), BoxGroup("Horizontal/Box1")]
    public GameObject baz;

    [HorizontalGroup("Horizontal"), BoxGroup("Horizontal/Box2")]
    public float alpha;

    [HorizontalGroup("Horizontal"), BoxGroup("Horizontal/Box2")]
    public Vector3 beta;

    [HorizontalGroup("Horizontal"), BoxGroup("Horizontal/Box2")]
    public GameObject gamma;
}
```

![img](../../images/img-group-2.png)