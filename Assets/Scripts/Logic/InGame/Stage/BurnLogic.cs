using System;
using System.Text;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.InGame.Stage
{
    public class BurnLogic : IBurnLogic
    {
        public BurnLogic
        (
            IStageTileMapModel stageTileMapModel,
            IGridCastLogic gridCastLogic
        )
        {
            StageTileMapModel = stageTileMapModel;
            GridCastLogic = gridCastLogic;
        }

        public ReadOnlySpan<SpawnCommand> Update(int floor, UpdateArgument updateArgument)
        {
            int count = 0;

            var fireIndex = updateArgument.MapIndex;
            var aroundTip = StageTileMapModel.GetAround4Tips(floor, fireIndex.x, fireIndex.y);

            foreach (var (position, tipBase) in aroundTip)
            {
                var builder = new StringBuilder();
                // マップチップは燃えるものか
                if (tipBase is not ITipBurnable burnable) continue;

                // そこにある`Pawn`は燃え広がるものか
                var castResult = GridCastLogic.CastGrid(floor, position, Vector2Int.one, CastTargetType.Pawn);
                var isBurning = false;
                foreach (var collider in castResult)
                {
                    if (collider.PawnType.IsBlock() | collider.PawnType == PawnType.Fire)
                    {
                        isBurning = true;
                        break;
                    }

                    builder.AppendLine(collider.PawnType.ToString());
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

        private SpawnCommand[] SpawnCommands { get; } = new SpawnCommand[8];
        private IStageTileMapModel StageTileMapModel { get; }
        private IGridCastLogic GridCastLogic { get; }
    }
}