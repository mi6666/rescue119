using Structure.InGame;
using Structure.InGame.Stage;

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
}