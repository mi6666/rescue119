using Interface.LogicInterface.InGame;
using Structure.InGame;

namespace Logic.InGame.Stage
{
    public class FloorLogic: IFloorUpdateLogic
    {
        public void Update(FloorTileTip tileTip, UpdateArgument argument)
        {
            if (tileTip.IsBurning)
            {
                // todo 耐久値減少・燃え広がり
            }
        }
    }
}