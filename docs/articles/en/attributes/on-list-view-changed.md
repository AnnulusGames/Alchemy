# On List View Changed Attribute

Detects changes in collections and invokes methods accordingly. Refer to Unity's [ListView documentation](https://docs.unity3d.com/ScriptReference/UIElements.ListView.html) for details on each event.

> [!WARNING]
> Ensure that the types of arguments for each event exactly match the arguments listed below (or the ListView event arguments). Failure to match will result in errors and the method won't execute.

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

| Parameter | Description |
| - | - |
| OnItemChanged | Name of the method called when an item's value changes `(int index, T value)` |
| OnItemIndexChanged | Name of the method called when an item's index changes `(int before, int after)` |
| OnItemsAdded | Name of the method called when items are added `(IEnumerable<int> indices)` |
| OnItemsRemoved | Name of the method called when items are removed `(IEnumerable<int> indices)` |
| OnItemsChosen | Name of the method called when items are chosen by pressing Enter or double-clicking `(IEnumerable<object> items)` |
| OnItemsSourceChanged | Name of the method called when the original collection changes, such as its count `(no arguments)` |
| OnSelectionChanged  | Name of the method called when the selected items change `(IEnumerable<object> items)` |
| OnSelectedIndicesChanged  | Name of the method called when the selected indices change `(IEnumerable<int> indices)` |
