using Interface.ModelInterface.InGame;
using PlasticPipe.PlasticProtocol.Messages;
using Structure.InGame;

namespace Model.InGame.Stage
{
    public class FloorMoveContextModel: IFloorMoveContextModel
    {
        private StairType _innerType;
        public StairType StairType => _innerType;
        public void SetContext(StairType stairType)
        {
            _innerType = stairType;
        }
    }
}