# Alchemy

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/Header.png" width="800">

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

[English README is here](README.md)

## 概要

Alchemyは属性を使用したInspector拡張を提供するライブラリです。

属性ベースの簡単かつ強力なエディタ拡張を追加するほか、独自のシリアル化プロセスを介してあらゆる型(Dictionary, Hashset, Nullable, Tuple, etc...)をシリアル化し、Inspector上で編集することが可能になります。Source Generatorを用いて必要なコードを動的に生成するため、partialにした対象型に属性を付加するだけで機能します。Odinのように専用のクラスを継承する必要はありません。

## 特徴

* Inspectorを拡張する30以上の属性を追加
* SerializeReferenceをサポートし、ドロップダウンから型を選択可能に
* あらゆる型(Dictionary, Hashset, Nullable, Tuple, etc...)をシリアル化/Inspectorで編集可能

## セットアップ

### 要件

* Unity 2021.2 以上 (シリアル化拡張を使用する場合、2022.1以上を推奨)
* Serialization 2.0 以上 (シリアル化拡張を使用する場合)

### インストール

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力する

```
https://github.com/AnnulusGames/Alchemy.git?path=/Alchemy/Assets/Alchemy
```

あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記

```json
{
    "dependencies": {
        "com.annulusgames.alchemy": "https://github.com/AnnulusGames/Alchemy.git?path=/Alchemy/Assets/Alchemy"
    }
}
```

### サンプル

Package Managerから全属性の挙動を確認できるサンプルを入手できます。詳しい使用方法はこちらのサンプルを参考にしてみてください。

<img src="https://github.com/AnnulusGames/Alchemy/blob/main/Alchemy/Assets/Alchemy/Documentation~/img7.png" width="500">


## 基本的な使い方

Inspectorでの表示をカスタマイズしたい場合には、クラスが持つフィールドに属性を付加します。

```cs
using UnityEngine;
using UnityEngine.UIElements;
using Alchemy.Inspector;  // Alchemy.Inspector名前空間をusingに追加

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

各フィールドをグループ化する属性もいくつか用意されています。各グループはスラッシュで区切ることでネストできます。

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

メソッドに`[Button]`属性を付加することで、メソッドをInspectorから実行することが可能になります。

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

利用可能な属性はこちらから確認できます。(現在wikiを作成中です)

## インターフェース/抽象クラスを編集する

`[SerializeReference]`属性を付加することでインターフェースや抽象クラスをInspector上で編集できるようになります。

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

インターフェース・抽象クラスは上のように表示され、ドロップダウンから子クラスを選択して生成することができます。

SerializeReferenceのシリアル化についてはUnityの公式マニュアルを参照してください。

## シリアル化拡張を使用する

Dictionaryなどの通常のUnityがシリアル化できない型を編集したい場合、`[AlchemySerialize]`属性を使用してシリアル化を行うことができます。

シリアル化拡張を使用したい場合、[Unity.Serialization](https://docs.unity3d.com/Packages/com.unity.serialization@3.1/manual/index.html)パッケージが必要になります。また、リフレクションを用いたUnity.Serializationのシリアル化はUnity2022.1以前のAOT環境で動作しない可能性があります。詳細はパッケージのマニュアルを確認してください。

以下は、Alchemyのシリアル化拡張を用いて様々な型をシリアル化/Inspectorで編集可能にするサンプルです。

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Serialization; // Alchemy.Serialization名前空間をusingに追加

// [AlchemySerialize]属性を付加することでAlchemyのシリアル化拡張が有効化されます。
// 任意の基底クラスを持つ型に使用できますが、SourceGeneratorがコード生成を行うため対象型はpartialである必要があります。
[AlchemySerialize]
public partial class AlchemySerializationSample : MonoBehaviour
{
    // 対象のフィールドに[AlchemySerializeField]属性と[NonSerialized]属性を付加します。
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

現在Inspectorで編集可能な型は以下の通りです。

* プリミティブ型
* UnityEngine.Object
* 配列
* List<>
* Hashset<>
* Dictionary<,>
* ValueTuple<>
* Nullable<>
* 以上の型のフィールドで構成されるclass/struct

シリアル化プロセスの技術的な詳細については下の項目を参照してください。

## Alchemyのシリアル化プロセス

Alchemyでは、対象の型に`[AlchemySerialize]`属性を付加することで専用のSource Generatorが`ISerializationCallbackReceiver`を自動で実装します。この処理の中で`[AlchemySerializeField]`属性が付加されたフィールドを全て取得し、Unity.Serializationパッケージを用いてJson形式に変換します。ただし、UnityEngine.Objectのフィールドに関してはJson形式で扱うことができないため、単一のListに実体を保存しJsonにはそのindexを書き込みます。

例えば、以下のようなクラスがあったとします。

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

これに対し、Alchemyは以下のようなコードを生成します。

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

このような手法を取っているため`[AlchemySerializeField]`を使用するとシリアライズ/デシリアライズにかかる処理負荷が増加します。そのため可能な限り`[AlchemySerializeField]`の使用を避けることが推奨されます。

また`[ShowAlchemySerializationData]`属性を付加することで、Inspector上からシリアル化データを確認することができるようになります。

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

## AlchemyEditorを拡張する
対象のMonoBehaviourやScriptableObjectが独自のEditorクラスを持つ場合、Alchemyの属性は動作しません。
独自のエディタ拡張とAlchemyを組み合わせたい場合には、`UnityEngine.Editor`クラスではなく`AlchemyEditor`クラスを継承する必要があります。

```cs
using UnityEditor;
using Alchemy.Editor;

[CustomEditor(typeof(Example))]
public class EditorExample : AlchemyEditor
{
   public override VisualElement CreateInspectorGUI()
    {
        // 必ず継承元のCreateInspectorGUIを呼び出す
        base.CreateInspectorGUI();

        // ここに独自の処理を記述する
    }
}
```

## デフォルトのエディタを無効化する

デフォルトではAlchemyでは独自のEditorクラスを使用して全ての型の描画を行います。ただし、他ライブラリやアセットとの競合を避けるためこれを無効化することもできます。

デフォルトのエディタを無効化するには、`Project Settings > Player > Scripting Define Symbols`の項目に`ALCHEMY_DISABLE_DEFAULT_EDITOR`を追加します。これを追加した状態でAlchemyの機能を使用したい場合には、`AlchemyEditor`を継承した独自のEditorクラスを定義する必要があります。

## ライセンス

[MIT License](LICENSE)
