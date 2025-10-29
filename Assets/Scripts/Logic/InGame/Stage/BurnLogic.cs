using System;
using Interface.LogicInterface.InGame;
using Structure.InGame;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.InGame.Stage
{
    public class BurnLogic : IBurnLogic
    {
        public BurnLogic(IGridCastLogic gridCastLogic)
        {
            GridCastLogic = gridCastLogic;
        }

        public ReadOnlySpan<FeedBackCommand> Update(ITipBurnable tipBurnable, UpdateArgument updateArgument)
        {
            if (!tipBurnable.IsBurn)
            {
                return CommandBuffer.AsSpan(0, 0);
            }

            int count = 0;

            var tipPos = updateArgument.MapIndex;
            var aroundTip = updateArgument.StageMap.GetAround4Tips(tipPos.x, tipPos.y);

            foreach (var (_, tipBase) in aroundTip)
            {
                if (tipBase is not ITipBurnable burnable) continue;
                if (burnable.IsBurn) continue;

                var toBurnAround = Random.Range(0, 100) >= 80;
                if (toBurnAround)
                {
                    burnable.SetBurn();
                    CommandBuffer[count] = new FeedBackCommand(burnable.InstanceId, TileStateType.Burning);
                    count++;
                }
            }


            return CommandBuffer.AsSpan(0, count);
        }

        public ReadOnlySpan<SpawnCommand> Update(int floor, UpdateArgument updateArgument)
        {
            int count = 0;

            var fireIndex = updateArgument.MapIndex;
            var aroundTip = updateArgument.StageMap.GetAround4Tips(fireIndex.x, fireIndex.y);

            foreach (var (position, tipBase) in aroundTip)
            {
                if (tipBase is not ITipBurnable burnable) continue;
                
                var castResult = GridCastLogic.CastGrid(floor, position, Vector2Int.one, CastTargetType.Pawn);
                var isBurning = false;
                foreach (var collider in castResult)
                {
                    if (collider.PawnType == PawnType.Fire)
                    {
                        isBurning = true;
                        break;
                    }
                }

                if (isBurning) continue;

                var toBurnAround = Random.Range(0, 100) >= 80;
                if (toBurnAround)
                {
                    burnable.SetBurn();
                    SpawnCommands[count] = new SpawnCommand(PawnType.Fire, position);
                    count++;
                }
            }

            return SpawnCommands.AsSpan(0, count);
        }

        private FeedBackCommand[] CommandBuffer { get; } = new FeedBackCommand[8];
        private SpawnCommand[] SpawnCommands { get; } = new SpawnCommand[8];
        private IGridCastLogic GridCastLogic { get; }
    }
}