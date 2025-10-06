using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Module.StateMachine;
using Structure.InGame;
using UnityEngine;

namespace Controller.InGame.Stage
{
    /// <summary>
    /// <para>タイルの更新処理</para>
    /// <para></para>
    /// </summary>
    public class NormalStateController : StageStateBehaviour
    {
        public NormalStateController
        (
            IStageTileMapModel stageTileMapModel,
            IStageFloorModel stageFloorModel,
            IFloorUpdateLogic floorUpdateLogic,
            IWallUpdateLogic wallUpdateLogic,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.Normal, innerState)
        {
            StageTileMapModel = stageTileMapModel;
            StageFloorModel = stageFloorModel;
            FloorUpdateLogic = floorUpdateLogic;
            WallUpdateLogic = wallUpdateLogic;
        }

        public override void StateUpdate(float deltaTime)
        {
            if (StageTileMapModel.StageMaps is null) return;
            
            var stageMap = StageTileMapModel.StageMaps[StageFloorModel.CurrentFloor];

            for (var y = 0; y < stageMap.LengthY; y++)
            {
                for (var x = 0; x < stageMap.LengthX; x++)
                {
                    if (!stageMap.GetTip(x, y).TryGetValue(out var tip)) continue;
                    var arg = new UpdateArgument(
                        stageMap,
                        new Vector2Int(x, y),
                        deltaTime
                    );

                    switch (tip)
                    {
                        case FloorTileTip floorTileTip:
                            FloorUpdateLogic.Update(floorTileTip, arg);
                            break;
                        case WallTileTip wallTileTip:
                            WallUpdateLogic.Update(wallTileTip,arg);
                            break;
                    }
                }
            }
        }

        private IStageTileMapModel StageTileMapModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IFloorUpdateLogic FloorUpdateLogic { get; }
        private IWallUpdateLogic WallUpdateLogic { get; }
    }
}