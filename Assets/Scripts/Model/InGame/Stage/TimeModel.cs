using System;
using Interface.ModelInterface.InGame;

namespace Model.InGame.Stage
{
    [Serializable]
    public class TimeModel : ITimeModel
    {
        public float CurrentTime { get; private set; }

        public void CountUpTime(float deltaTime)
        {
            CurrentTime += deltaTime;
        }
    }
}