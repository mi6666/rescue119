using System;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.LogicInterface.InGame
{
    public interface IBurnLogic
    {
        public ReadOnlySpan<FeedBackCommand> Update(ITipBurnable tipBurnable, UpdateArgument updateArgument);
    }

    public readonly struct FeedBackCommand
    {
        public int  ObjectId { get; }
        public TileStateType StateType { get; }

        public FeedBackCommand
        (
            int objectId,
            TileStateType stateType
        )
        {
            ObjectId = objectId;
            StateType = stateType;
        }
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