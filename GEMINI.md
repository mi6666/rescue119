# プロジェクト概要: rescue119

このドキュメントは、`rescue119` Unityプロジェクトの技術スタック、アーキテクチャ、主要機能について概説します。

## 1. プロジェクト情報

*   **Unity バージョン**: `6000.2.5f1`
*   **オペレーティングシステム**: Linux

## 2. 主要技術とライブラリ

このプロジェクトは、開発効率と機能性を向上させるために、いくつかの最新のUnityパッケージとサードパーティライブラリを活用しています。

*   **UniTask (`com.cysharp.unitask`)**: 効率的な非同期プログラミングのため。従来のC#の`async/await`を、ゲーム開発に適した最適化されたアロケーションフリーな代替手段に置き換えます。
*   **VContainer (`jp.hadashikick.vcontainer`)**: Unity用の高速かつ軽量な依存性注入（DI）コンテナで、モジュール化されたテスト可能なコードを促進します。
*   **Unity Input System (`com.unity.inputsystem`)**: 柔軟でカスタマイズ可能な入力処理を提供する、Unityの最新の入力管理システムです。
*   **Universal Render Pipeline (URP) (`com.unity.render-pipelines.universal`)**: パフォーマンスとスケーラビリティのために最適化されたスクリプタブルレンダーパイプラインで、ここでは2Dレンダラーと共に使用されています。
*   **LitMotion (`com.annulusgames.lit-motion`)**: 高性能なトゥイーン/アニメーションライブラリです。
*   **R3 (`org.nuget.r3`)**: リアクティブプログラミングライブラリです。
*   **Unity 2D パッケージ**: `com.unity.2d.animation`、`com.unity.2d.aseprite`、`com.unity.2d.psdimporter`、`com.unity.2d.sprite`、`com.unity.2d.spriteshape`、`com.unity.2d.tilemap`を含み、包括的な2Dゲーム開発をサポートします。
*   **Unity UI Toolkit (`com.unity.ugui`)**: Unityの標準UIシステムです。
*   **Unity Visual Scripting (`com.unity.visualscripting`)**: ビジュアルプログラミングワークフロー用です。

## 3. プロジェクトアーキテクチャ

このプロジェクトは、Unity Assembly Definitions (`.asmdef`) を使用して関心を明確なレイヤーに分離するクリーンアーキテクチャアプローチを採用しており、モジュール性、保守性、テスト容易性を向上させています。

*   **`Interface` レイヤー**:
    *   `LogicInterface`: ビジネスロジック層のインターフェースを定義します。
    *   `ModelInterface`: データおよび状態管理層のインターフェースを定義します。
    *   `ViewInterface`: プレゼンテーション層のインターフェースを定義します。
    このレイヤーは、アプリケーションの異なる部分間の疎結合を保証します。

*   **`Model` レイヤー**:
    *   アプリケーションのデータ、状態、およびコアビジネスルールを管理します。
    *   `ModelInterface`を参照します。

*   **`Logic` レイヤー**:
    *   ModelとView間の相互作用を調整する主要なアプリケーションロジックを含みます。
    *   `ModelInterface`、`LogicInterface`、および`Structure`を参照します。

*   **`View` レイヤー**:
    *   UIとゲームオブジェクトのレンダリング、およびユーザー入力への反応を担当します。
    *   `ViewInterface`および`Structure`を参照します。

*   **`Controller` レイヤー**:
    *   仲介役として機能し、Viewからの入力を受け取り、それをLogic/Modelへのコマンドに変換します。
    *   `LogicInterface`、`ViewInterface`、`ModelInterface`、および`Structure`を参照します。

*   **`Structure` レイヤー**:
    *   複数のレイヤー間で共有される共通のデータ構造、列挙型、またはユーティリティクラスを含みます。

*   **`Installer` レイヤー**:
    *   依存性注入のために`VContainer`を利用します。`GlobalInstaller.cs`と`InstallerBase.cs`が存在し、依存性解決とオブジェクトグラフ構築への構造化されたアプローチを示しています。

*   **`Module` レイヤー**:
    *   以下を含む様々な再利用可能なモジュールを格納します。
        *   `EditorExtension`: エディタ固有のユーティリティ。
        *   `Option`: 設定またはオプション管理。
        *   `SceneReference`: シーン参照を管理するためのユーティリティ。
        *   `StateMachine`: 複雑なオブジェクトの振る舞いやアプリケーションの状態を管理するための堅牢なステートマシン実装（`AbstractAsyncStateMachine.cs`、`AbstractStateMachine.cs`、`Interfaces.cs`、`StateBehaviour.cs`）。

## 4. シーン構造

プロジェクトはシーンを論理的に整理しています。

*   `Assets/Scenes/Enviroment/InGame/Env_Stage.unity`: ゲーム内ステージの環境要素。
*   `Assets/Scenes/Primary/InGame/Stage1.unity`: メインのゲームプレイステージ。
*   `Assets/Scenes/UserInterface/Ui_InGame.unity`: ゲーム内のユーザーインターフェース要素。
*   `Assets/Scenes/SampleScene.unity`: デフォルトまたはテストシーン。

## 5. レンダリングパイプライン

このプロジェクトは、**Universal Render Pipeline (URP)** を **2Dレンダラー** と共に使用するように構成されており、最適化された2Dグラフィックスに重点を置いていることを示しています。