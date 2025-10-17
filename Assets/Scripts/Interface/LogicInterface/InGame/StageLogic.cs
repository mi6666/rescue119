using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.LogicInterface.InGame
{

    public interface IBurnLogic
    {
        public void Update(ITipBurnable tipBurnable, UpdateArgument updateArgument);
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