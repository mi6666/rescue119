using System;
using Interface.LogicInterface.InGame;
using Structure.InGame;
using Structure.InGame.Stage;
using Random = UnityEngine.Random;

namespace Logic.InGame.Stage
{
    public class FloorLogic : IBurnLogic
    {
        public ReadOnlySpan<FeedBackCommand> Update(ITipBurnable tipBurnable, UpdateArgument updateArgument)
        {
            int count = 0;

            var tipPos = updateArgument.MapIndex;
            var aroundTip = updateArgument.StageMap.GetAroundTips(tipPos.x, tipPos.y);

            for (int i = 0; i < aroundTip.Length; i++)
            {
                if (aroundTip[i] is ITipBurnable burnable)
                {
                    if (!burnable.IsBurn)
                    {
                        var toBurnAround = Random.Range(0, 100) >= 80;
                        if (toBurnAround)
                        {
                            burnable.SetBurn();
                            commandbuffer[count] = new FeedBackCommand(burnable.InstanceId, TileStateType.Burning);
                            count++;
                        }
                        
                    }
                }
            }


            return commandbuffer.AsSpan(0, count);
        }

        private FeedBackCommand[] commandbuffer { get; }
    }
}