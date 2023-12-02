using UnityEngine;
using Alchemy.Inspector;

namespace Alchemy.Samples
{
    public class GroupAttributesSample : MonoBehaviour
    {
        [FoldoutGroup("Foldout")] public float a;
        [FoldoutGroup("Foldout")] public Vector3 b;
        [FoldoutGroup("Foldout")] public GameObject c;

        [TabGroup("Tab", "Tab1")] public float x;
        [TabGroup("Tab", "Tab2")] public Vector3 y;
        [TabGroup("Tab", "Tab3")] public GameObject z;

        [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box1")] public float foo;
        [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box1")] public Vector3 bar;
        [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box1")] public GameObject baz;

        [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box2")] public float alpha;
        [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box2")] public Vector3 beta;
        [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box2")] public GameObject gamma;
    }
}
