using Interface.ModelInterface.InGame;

namespace Model.InGame.Stage
{
    public class EmptyStageMasterModel: IStageMasterModel
    {
        public float TimeLength => 100f;
        public int MaxFloorNum => int.MaxValue;
        public float PawnTickInterval => 100f;
    }
}