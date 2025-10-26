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
    *   `PresenterInterface`: 
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

*   **`Presenter` レイヤー**:
    *   ViewとController間の調整役として機能します。シーンの読み込みやUIイベントの管理などを制御します。
    *   `ViewInterface`, `PresenterInterface`, `Structure` を参照します。

*   **`Controller` レイヤー**:
    *   ユーザー入力や特定のゲーム内イベントに応じて、`Logic`や`Model`を直接操作する、より具体的なコンポーネントです。ステートマシンを用いてキャラクターの振る舞いを管理するなど、特定のドメインロジックを担当します。
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

## 6. ドメイン言語

このプロジェクトに特有の主要な概念や用語は以下の通りです。

### コアコンセプト

*   **Tip (ティップ)**: ステージを構成するタイルマップの各マス、およびそのマスが持つ情報全体を指す言葉。`FloorTip`、`WallTip`、`RubbleTip` のように、タイルの種類ごとに具体的な`Tip`が存在します。
*   **Pawn (ポーン)**: ステージ上に存在する、タイルではないキャラクターやオブジェクト（特に救助対象者など）を指します。これらのオブジェクトはグリッドベースで管理されます。
*   **Burn / Burning (燃焼)**: ステージ上のタイルが「燃える」という概念。延焼のロジックも存在し、ゲームの重要な要素です。
*   **Splash Water (放水)**: プレイヤーの`Action`ステートで行われる、水を撒くアクション。おそらく「燃焼」を鎮めるための行動です。
*   **Rubble (瓦礫)**: ステージを構成するタイルの一種であり、障害物としての役割を持つ要素です。

### 状態管理

プロジェクトではステートマシンが多用されており、以下の状態が定義されています。

*   **PrimaryState**: ゲーム全体の進行状態 (`Normal`, `Pause`, `GameClear`, `GameOver`, `FloorMove`)。
*   **PlayerState**: プレイヤーの行動状態 (`Normal`, `Action`)。
*   **StageState**: ステージギミックや環境の状態 (`EntryPoint`, `Normal`, `FloorTransition`)。

### システム・構造

*   **GridCast / GridCollider**: タイルマップ上のグリッド（マス目）単位で行われる当たり判定の仕組み。
*   **SceneGroup**: 1つのプライマリシーンと、それに付随する複数のサブシーン（UIや環境など）をグループとして扱うための単位。
