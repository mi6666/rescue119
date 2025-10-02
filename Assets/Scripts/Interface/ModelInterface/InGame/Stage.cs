using Structure.InGame;

namespace Interface.ModelInterface.InGame
{
    public interface IStageSettingModel
    {
        public float TimeLength { get; }
    }

    public interface ITimeModel
    {
        public float CurrentTime { get; }
        public void CountUpTime(float deltaTime);
    }

    public interface IStageTileMapModel
    {
        public StageMap[] StageMaps { get; }
    }

    public interface IStageFloorModel
    {
        public int CurrentFloor { get; }

        public void SetFloor(int floor);
    }
}