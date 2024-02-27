using System.Collections.Generic;
using UnityEngine;
using Alchemy.Inspector;

public class OnListViewChangedTest : MonoBehaviour
{
    [OnListViewChanged(
        OnItemChanged = nameof(OnItemChanged),
        OnItemsAdded = nameof(OnItemsAdded),
        OnItemsRemoved = nameof(OnItemsRemoved),
#if UNITY_2022_1_OR_NEWER
        OnSelectedIndicesChanged = nameof(OnSelectedIndicesChanged),
#endif

        OnItemIndexChanged = nameof(OnItemIndexChanged))
    ]
    public int[] array;

    void OnItemChanged(int index, int item)
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
#if UNITY_2022_1_OR_NEWER
    void OnSelectedIndicesChanged(IEnumerable<int> indices)
    {
        Debug.Log($"Selected: [{string.Join(',', indices)}]");
    }
#endif
    void OnItemIndexChanged(int before, int after)
    {
        Debug.Log($"Index Changed: [{before} -> {after}]");
    }
}
