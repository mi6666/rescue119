using Interface.ModelInterface.InGame;

namespace Model.InGame.Stage
{
    public class TimeModel : ITimeModel
    {
        public TimeModel(IStageMasterModel masterModel)
        {
            StageMasterModel = masterModel;
        }

        private void Init()
        {
            CurrentTime = 0f;
        }

        public float CurrentTime { get; private set; }
        public float TimeLength => StageMasterModel.TimeLength;

        public void CountUpTime(float deltaTime)
        {
            CurrentTime += deltaTime;
        }
        
        private IStageMasterModel StageMasterModel { get; }
    }
}