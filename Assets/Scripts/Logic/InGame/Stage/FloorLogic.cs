using Interface.LogicInterface.InGame;
using Structure.InGame;
using UnityEngine;

namespace Logic.InGame.Stage
{
    public class FloorLogic: IFloorUpdateLogic,IWallUpdateLogic
    {
        public void Update(FloorTileTip tileTip, UpdateArgument argument)
        {
            if (tileTip.IsBurning)
            {
                var toBurnAround = Random.Range(0, 100) >= 80;

                if (toBurnAround)
                {
                    var tipPos = argument.MapIndex;
                    var aroundTip = argument.StageMap.GetAroundTips(tipPos.x, tipPos.y);
                    for (int i = 0; i < aroundTip.Length; i++)
                    {
                        if (aroundTip[i] is IBurnable burnable)
                        {
                            burnable.SetBurn();
                        }
                    }
                }
            }
        }

        public void Update(WallTileTip tileTip, UpdateArgument argument)
        {
            if (tileTip.IsBurning)
            {
                var toBurnAround = Random.Range(0, 100) >= 80;

                if (toBurnAround)
                {
                    var tipPos = argument.MapIndex;
                    var aroundTip = argument.StageMap.GetAroundTips(tipPos.x, tipPos.y);
                    for (int i = 0; i < aroundTip.Length; i++)
                    {
                        if (aroundTip[i] is IBurnable burnable)
                        {
                            burnable.SetBurn();
                        }
                    }
                }
            }
        }
    }
}