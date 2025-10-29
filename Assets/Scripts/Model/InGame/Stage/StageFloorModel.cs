using Interface.ModelInterface.InGame;
using R3;

namespace Model.InGame.Stage
{
    public class StageFloorModel : IStageFloorModel
    {
        public int CurrentFloor => Floor.CurrentValue;
        public ReadOnlyReactiveProperty<int> FloorObservable => Floor;

        public void SetFloor(int floor)
        {
            Floor.Value = floor;
        }

        private ReactiveProperty<int> Floor { get; } = new ();
    }
}