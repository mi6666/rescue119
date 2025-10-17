using Interface.LogicInterface.InGame;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace Logic.InGame.Stage
{
    public class FloorLogic: IBurnLogic
    {
        /*public void Update(FloorTip tileTip, UpdateArgument argument)
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

        public void Update(WallTip tileTip, UpdateArgument argument)
        {
            if (tileTip.IsBurning)
            {
                
            }
        }
    */
        
        // FIXME:
        // 燃えているなら
        // インターフェースの移行
        public void Update(ITipBurnable tipBurnable, UpdateArgument updateArgument)
        {
            var toBurnAround = Random.Range(0, 100) >= 80;

            if (toBurnAround)
            {
                var tipPos = updateArgument.MapIndex;
                var aroundTip = updateArgument.StageMap.GetAroundTips(tipPos.x, tipPos.y);
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