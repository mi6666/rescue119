using System;
using Module.Option.Runtime;
using R3;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.ModelInterface.InGame
{
    // ============================================================================================
    // 設定情報
    // ============================================================================================
    /// <summary>
    /// ステージ設定を保持するモデル
    /// </summary>
    public interface IStageMasterModel
    {
        public float TimeLength { get; }
        public int MaxFloorNum { get; }
        public float PawnTickInterval { get; }
    }
    
    // ============================================================================================
    // 実行時情報
    // ============================================================================================
    
    //
    /// <summary>
    /// 経過時間を保持するモデル
    /// </summary>
    public interface ITimeModel
    {
        public float TimeLength { get; }
        public float CurrentTime { get; }
        public void CountUpTime(float deltaTime);
    }

    /// <summary>
    /// ステージのマップタイルのモデル
    /// </summary>
    public interface IStageTileMapModel
    {
        public void InitStageMap(FloorMap[] stageMaps);
        public ReadOnlySpan<(Vector2Int, TipBase)> GetAround4Tips(int floor, int x, int y);
        public Option<TipBase> GetTip(int floor, Vector2Int position);
    }

    /// <summary>
    /// ステージの階層を保持するモデル
    /// </summary>
    public interface IStageFloorModel
    {
        public int CurrentFloor { get; }
        public ReadOnlyReactiveProperty<int> FloorObservable { get; }

        public void SetFloor(int floor);
    }

    /// <summary>
    /// フロア移動時の情報を保持する
    /// </summary>
    public interface IFloorMoveContextModel
    {
        public StairType StairType { get; }

        public void SetContext(StairType stairType);
    }

    /// <summary>
    /// ステージ上に存在するタイルではないオブジェクトに関する情報を持つ
    /// </summary>
    public interface IStagePawnModel
    {
        public ReadOnlySpan<GridCollider> Pawns { get; }
        public void StorePawn(GridCollider gridCollider);
        public void RemovePawn(int id);
    }
}