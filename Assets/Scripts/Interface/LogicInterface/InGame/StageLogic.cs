using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.LogicInterface.InGame
{
    public interface IFloorUpdateLogic
    {
        public void Update(FloorTileTip tileTip, UpdateArgument argument);
    }

    public interface IWallUpdateLogic
    {
        public void Update(WallTileTip tileTip, UpdateArgument argument);
    }

    public readonly ref struct UpdateArgument
    {
        public StageMap StageMap { get; }
        public Vector2Int MapIndex { get; }
        public float DeltaTime { get; }

        public UpdateArgument
        (
            StageMap stageMap,
            Vector2Int mapIndex,
            float deltaTime
        )
        {
            StageMap = stageMap;
            MapIndex = mapIndex;
            DeltaTime = deltaTime;
        }
    }
}