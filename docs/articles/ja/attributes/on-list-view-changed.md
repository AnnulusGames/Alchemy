# On List View Changed Attribute

コレクションの変更を検知してメソッドを呼び出します。各イベントの詳細はUnityの[ListViewのドキュメント](https://docs.unity3d.com/ScriptReference/UIElements.ListView.html)を参照してください。

> [!WARNING]
> 各イベントの引数の型が、下の表に示した引数(またはListViewのイベントの引数)と完全に一致していることを確認してください。一致しない場合、メソッドを実行できずエラーが発生します。

```cs 
[OnListViewChanged(
    OnItemChanged = nameof(OnItemChanged),
    OnItemsAdded = nameof(OnItemsAdded),
    OnItemsRemoved = nameof(OnItemsRemoved),
    OnSelectedIndicesChanged = nameof(OnSelectedIndicesChanged),
    OnItemIndexChanged = nameof(OnItemIndexChanged))
]
public float[] array;

void OnItemChanged(int index, float item)
{
    Debug.Log($"Changed: [{index}] -> {item}");
}

void OnItemsAdded(IEnumerable<int> indices)
{
    Debug.Log($"Added: [{string.Join(',', indices)}]");
}

void OnItemsRemoved(IEnumerable<int> indices)
{
    Debug.Log($"Removed: [{string.Join(',', indices)}]");
}

void OnSelectedIndicesChanged(IEnumerable<int> indices)
{
    Debug.Log($"Selected: [{string.Join(',', indices)}]");
}

void OnItemIndexChanged(int before, int after)
{
    Debug.Log($"Index Changed: [{before} -> {after}]");
}
```

| パラメータ | 説明 |
| - | - |
| OnItemChanged | 要素の値を変更した際に呼ばれるメソッドの名前 `(int index, T value)` |
| OnItemIndexChanged | 要素のindexが変更された際に呼ばれるメソッドの名前 `(int before, int after)` |
| OnItemsAdded | 要素が追加された際に呼ばれるメソッドの名前 `(IEnumerable<int> indices)` |
| OnItemsRemoved | 要素が削除された際に呼ばれるメソッドの名前 `(IEnumerable<int> indices)` |
| OnItemsChosen | 要素がEnterキーやダブルクリックで選択された際に呼ばれるメソッドの名前 `(IEnumerable<object> items)` |
| OnItemsSourceChanged | 要素の個数など、元のコレクションが変更された際に呼ばれるメソッドの名前 `(引数なし)` |
| OnSelectionChanged  | 選択中の要素が変更された際に呼ばれるメソッドの名前 `(IEnumerable<object> items)` |
| OnSelectedIndicesChanged  | 選択中のindexが変更された際に呼ばれるメソッドの名前 `(IEnumerable<int> indices)` |
