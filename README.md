# Alchemy

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/Header.png" width="800">

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

[日本語版READMEはこちら](README_JA.md)

## Overview

Alchemy is a library that provides Inspector extensions using attributes.

In addition to adding easy and powerful editor extensions based on attributes, it allows serialization of various types (Dictionary, HashSet, Nullable, Tuple, etc.) through its custom serialization process, enabling editing within the Inspector. By using Source Generator to dynamically generate necessary code, it functions by simply adding attributes to the targeted type made partial. There's no need to inherit specialized classes similar to Odin.

## Features

* Adds over 30 attributes to extend the Inspector
* Supports SerializeReference, allowing type selection from dropdown
* Enables serialization/editing of various types (Dictionary, HashSet, Nullable, Tuple, etc.)

## Setup

### Requirements

* Unity 2021.2 or later (2022.1 or later recommended for serialization extension)
* Serialization 2.0 or later (for serialization extension)

### Installation

1. Open Package Manager from Window > Package Manager.
2. Click on the "+ Add package from git URL" button.
3. Enter the following URL:

```
https://github.com/AnnulusGames/Alchemy.git?path=/Alchemy/Assets/Alchemy
```

Alternatively, open Packages/manifest.json and add the following to the dependencies block:

```json
{
    "dependencies": {
        "com.annulusgames.alchemy": "https://github.com/AnnulusGames/Alchemy.git?path=/Alchemy/Assets/Alchemy"
    }
}
```

### Samples

You can obtain a sample to verify the behavior of all attributes from the Package Manager. Please refer to this sample for detailed usage instructions.

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img7.png" width="500">

## Basic Usage

To customize the display in the Inspector, add attributes to the fields of the class.

```cs
using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;  // Added Alchemy.Inspector namespace

public class BasicAttributesSample : MonoBehaviour
{
    [LabelText("Custom Label")]
    public float foo;

    [HideLabel]
    public Vector3 bar;
    
    [AssetsOnly]
    public GameObject baz;

    [Title("Title")]
    [HelpBox("HelpBox", HelpBoxMessageType.Info)]
    [ReadOnly]
    public string message = "Read Only";
}
```

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img1.png" width="600">

Several attributes are provided to group each field. Each group can be nested by separating with a slash.

```cs
using UnityEngine;
using Alchemy.Inspector;

public class GroupAttributesSample : MonoBehaviour
{
    [FoldoutGroup("Foldout")] public int a;
    [FoldoutGroup("Foldout")] public int b;
    [FoldoutGroup("Foldout")] public int c;

    [TabGroup("Tab", "Tab1")] public int x;
    [TabGroup("Tab", "Tab2")] public string y;
    [TabGroup("Tab", "Tab3")] public Vector3 z;

    [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box1")] public float foo;
    [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box1")] public Vector3 bar;
    [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box1")] public GameObject baz;

    [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box2")] public float alpha;
    [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box2")] public Vector3 beta;
    [HorizontalGroup("Horizontal")][BoxGroup("Horizontal/Box2")] public GameObject gamma;
}
```

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img2.png" width="600">

By adding the `[Button]` attribute to a method, the method can be executed from the Inspector.

```cs
using System.Text;
using UnityEngine;
using Alchemy.Inspector;

[Serializable]
public sealed class SampleClass : ISample
{
    public float foo;
    public Vector3 bar;
    public GameObject baz;
}

public class ButtonSample : MonoBehaviour
{
    [Button]
    public void Foo()
    {
        Debug.Log("Foo");
    }

    [Button]
    public void Foo(int parameter)
    {
        Debug.Log("Foo: " + parameter);
    }

    [Button]
    public void Foo(SampleClass parameter)
    {
        var builder = new StringBuilder();
        builder.AppendLine();
        builder.Append("foo = ").AppendLine(parameter.foo.ToString());
        builder.Append("bar = ").AppendLine(parameter.bar.ToString());
        builder.Append("baz = ").Append(parameter.baz == null ? "Null" : parameter.baz.ToString());
        Debug.Log("Foo: " + builder.ToString());
    }
}
```

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img3.png" width="600">

Available attributes can be checked [here](URL) (currently creating a wiki).

## Editing Interfaces/Abstract Classes

By adding the `[SerializeReference]` attribute, you can edit interfaces or abstract classes in the Inspector.

```cs
using UnityEngine;

public interface ISample { }

[Serializable]
public sealed class SampleA : ISample
{
    public float alpha;
}

[Serializable]
public sealed class SampleB : ISample
{
    public Vector3 beta;
}

[Serializable]
public sealed class SampleC : ISample
{
    public GameObject gamma;
}

public class SerializeReferenceSample : MonoBehaviour
{
    [SerializeReference] public ISample sample;
    [SerializeReference] public ISample[] sampleArray;
}
```

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img4.png" width="600">

Interfaces/abstract classes are displayed as shown above, and you can select and instantiate child classes from the dropdown.

For SerializeReference serialization details, refer to the Unity official manual.

## Using Serialization Extensions

If you want to edit types that Unity cannot serialize by default, like Dictionary, you can use `[AlchemySerialize]` attribute to perform serialization.

When using serialization extensions, the [Unity.Serialization](https://docs.unity3d.com/Packages/com.unity.serialization@3.1/manual/index.html) package is required. Additionally, Unity.Serialization serialization using reflection may not work in AOT environments before Unity 2022.1. Please refer to the package manual for details.

The following is a sample using Alchemy's serialization extensions to make various types serializable/editable in the Inspector.

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization; // Added Alchemy.Serialization namespace

// Adding the [AlchemySerialize] attribute enables Alchemy's serialization extensions.
// It can be used with any type that has a base class, but the target type must be partial as the SourceGenerator generates code.
[AlchemySerialize]
public partial class AlchemySerializationSample : MonoBehaviour
{
    // Add [AlchemySerializeField] attribute and [NonSerialized] attribute to the target fields.
    [AlchemySerializeField, NonSerialized]
    public HashSet<GameObject> hashset = new();

    [AlchemySerializeField, NonSerialized]
    public Dictionary<string, GameObject> dictionary = new();

    [AlchemySerializeField, NonSerialized]
    public (int, int) tuple;

    [AlchemySerializeField, NonSerialized]
    public Vector3? nullable = null;
}
```

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img5.png" width="600">

Currently, the following types are editable in the Inspector:

* Primitive types
* UnityEngine.Object
* Arrays
* List<>
* HashSet<>
* Dictionary<,>
* ValueTuple<>
* Nullable<>
* class/struct composed of the above types' fields

Refer to the following section for technical details of the serialization process.

## Alchemy's Serialization Process

In Alchemy, adding the `[AlchemySerialize]` attribute to the target type automatically implements `ISerializationCallbackReceiver` using a dedicated Source Generator. Within this process, all fields with the `[AlchemySerializeField]` attribute are retrieved, and Unity.Serialization package is used to convert them into Json format. However, for UnityEngine.Object fields, they cannot be handled in Json format. Therefore, their instances are saved into a single List, and their indexes are written into Json.

For instance, consider having a class like this:

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

[AlchemySerialize]
public partial class AlchemySerializationSample : MonoBehaviour
{
    [AlchemySerializeField, NonSerialized]
    public Dictionary<string, GameObject> dictionary = new();
}
```

Alchemy would generate code like this:

```cs
partial class AlchemySerializationSample : global::UnityEngine.ISerializationCallbackReceiver
{
    [global::System.Serializable]
    sealed class AlchemySerializationData
    {
        [global::System.Serializable]
        public sealed class Item
        {
            [global::UnityEngine.HideInInspector] public bool isCreated;
            [global::UnityEngine.TextArea] public string data;
        }

        public Item dictionary = new();

        [global::UnityEngine.SerializeField] private global::System.Collections.Generic.List<UnityEngine.Object> unityObjectReferences = new();

        public global::System.Collections.Generic.IList<UnityEngine.Object> UnityObjectReferences => unityObjectReferences;
    }

    [global::UnityEngine.HideInInspector, global::UnityEngine.SerializeField] private AlchemySerializationData alchemySerializationData =  new();

    void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        if (this is global::Alchemy.Serialization.IAlchemySerializationCallbackReceiver receiver) receiver.OnBeforeSerialize();
        alchemySerializationData.UnityObjectReferences.Clear();
        
        try
        {
            alchemySerializationData.dictionary.data = global::Alchemy.Serialization.Internal.SerializationHelper.ToJson(this.dictionary , alchemySerializationData.UnityObjectReferences);
            alchemySerializationData.dictionary.isCreated = true;
        }
        catch (global::System.Exception ex)
        {
            global::UnityEngine.Debug.LogException(ex);
        }
    }

    void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        try 
        {
            if (alchemySerializationData.dictionary.isCreated)
            {
                this.dictionary = global::Alchemy.Serialization.Internal.SerializationHelper.FromJson<System.Collections.Generic.Dictionary<string, UnityEngine.GameObject>>(alchemySerializationData.dictionary.data, alchemySerializationData.UnityObjectReferences);
            }
        }
        catch (global::System.Exception ex)
        {
            global::UnityEngine.Debug.LogException(ex);
        }

        if (this is global::Alchemy.Serialization.IAlchemySerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
    }
}
```

Using `[AlchemySerializeField]` increases the processing load for serialization/deserialization. Therefore, it's recommended to avoid using `[AlchemySerializeField]` as much as possible.

Additionally, adding `[ShowAlchemySerializationData]` attribute allows viewing serialization data from the Inspector.

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization;

[AlchemySerialize]
[ShowAlchemySerializationData]
public partial class AlchemySerializationSample : MonoBehaviour
{
    [AlchemySerializeField, NonSerialized]
    public Dictionary<string, GameObject> dictionary = new();
}
```

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img6.png" width="600">

## Extending AlchemyEditor

When a MonoBehaviour or ScriptableObject has its own Editor class, Alchemy attributes won't function as expected. If you wish to combine custom editor extensions with Alchemy, you need to inherit from the `AlchemyEditor` class rather than the `UnityEngine.Editor` class.

```cs
using UnityEditor;
using Alchemy.Editor;

[CustomEditor(typeof(Example))]
public class EditorExample : AlchemyEditor
{
   public override VisualElement CreateInspectorGUI()
    {
        // Always call the base class's CreateInspectorGUI
        base.CreateInspectorGUI();

        // Add your custom logic here
    }
}
```

## Disabling Default Editor

By default, Alchemy uses its own Editor class to render all types. However, it's possible to disable this behavior to avoid conflicts with other libraries or assets.

To disable the default editor, add `ALCHEMY_DISABLE_DEFAULT_EDITOR` to `Project Settings > Player > Scripting Define Symbols`. When this is added, if you want to use Alchemy's features, you'll need to define your custom Editor class inheriting from `AlchemyEditor`.

## Help

Unity forum: https://forum.unity.com/threads/released-alchemy-inspector-serialization-extensions.1523665/

## License

[MIT License](LICENSE)