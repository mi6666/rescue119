using Module.Option.Runtime;
using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.ModelInterface.InGame
{
    /// <summary>
    /// ステージ設定を保持するモデル
    /// </summary>
    public interface IStageSettingModel
    {
        public float TimeLength { get; }
        public int MaxFloorNum { get; }
    }

    /// <summary>
    /// 経過時間を保持するモデル
    /// </summary>
    public interface ITimeModel
    {
        public float CurrentTime { get; }
        public void CountUpTime(float deltaTime);
    }

    /// <summary>
    /// ステージのマップタイルのモデル
    /// </summary>
    public interface IStageTileMapModel
    {
        public StageMap[] StageMaps { get; }
        public void InitStageMap(StageMap[] stageMaps);
    }

    /// <summary>
    /// ステージの階層を保持するモデル
    /// </summary>
    public interface IStageFloorModel
    {
        public int CurrentFloor { get; }

        public void SetFloor(int floor);
    }

    /// <summary>
    /// ステージ上に存在するタイルではないオブジェクトに関する情報を持つ
    /// </summary>
    public interface IStagePawnModel
    {
        public void StorePawn(GridCollider gridCollider);
        public void RemovePawn(int id);
        public Option<int> CastPosition(Vector2Int position);
    }
}