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
}