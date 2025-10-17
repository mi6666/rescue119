using Interface.ModelInterface.InGame;

namespace Model.InGame.Stage
{
    public class StageFloorModel : IStageFloorModel
    {
        public int CurrentFloor => _currentFloor;

        public void SetFloor(int floor)
        {
            _currentFloor = floor;
        }

        private int _currentFloor;
    }
}