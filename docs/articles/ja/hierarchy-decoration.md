# Hierarchyの装飾

AlchemyではHierarchyにヘッダーや仕切り線を追加することで、Hierarchyをより見やすく、扱いやすいように装飾することが可能になっています。

![img](../../images/img-hierarchy.png)

ヘッダーや仕切り線を追加するには、Hierarchyの「+」ボタンから`Alchemy > Header/Separator`を選択します。

![img](../../images/img-create-hierarchy-object.png)

また、これらの装飾に使われるオブジェクトのことをAlchemyでは`HierarchyObject`と呼び、これらのオブジェクトはビルドからは除外されます。(子オブジェクトが存在する場合は`transform.DetachChildren()`を呼んで全ての子オブジェクトを解除した後に削除されます。)

`HierarchyObject`の扱いについては`Project Settings > Alchemy`から変更が可能です。

![img](../../images/img-project-settings.png)

個別の`HierarchyObject`の扱いを変更したい際には、各オブジェクトのInspectorから変更できます。

![img](../../images/img-hierarchy-header-inspector.png)
