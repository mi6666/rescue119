using Interface.ModelInterface.InGame;
using Structure.OutGame;

namespace Model.InGame.Stage
{
    public class EmptyStageMasterModel: IStageMasterModel
    {
        public float TimeLength => 100f;
        public int MaxFloorNum => int.MaxValue;
        public float PawnTickInterval => 100f;
        public float TickInterval(DifficultyLevel difficultyLevel)
        {
            return PawnTickInterval;
        }
    }
}