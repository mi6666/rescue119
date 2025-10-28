using System;
using Module.Option.Runtime;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.LogicInterface.InGame
{
    public interface IBurnLogic
    {
        public ReadOnlySpan<FeedBackCommand> Update(ITipBurnable tipBurnable, UpdateArgument updateArgument);
        public ReadOnlySpan<SpawnCommand> Update(int floor, UpdateArgument updateArgument);
    }

    public interface IGridCastLogic
    {
        public ReadOnlySpan<GridCollider> CastGrid(int floor, Vector2Int position, Vector2Int size,
            CastTargetType castTarget);

        public Option<GridCollider> CastGridFirst(int floor, Vector2Int position, Vector2Int size,
            CastTargetType castTarget);
    }

    public enum CastTargetType
    {
        Pawn,
        Tile,
    }

    public readonly struct FeedBackCommand
    {
        public int ObjectId { get; }
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

    public readonly struct SpawnCommand
    {
        public PawnType Type { get; }
        public Vector2Int MapIndex { get; }

        public SpawnCommand
        (
            PawnType type,
            Vector2Int mapIndex
        )
        {
            Type = type;
            MapIndex = mapIndex;
        }
    }

    public readonly ref struct UpdateArgument
    {
        public StageMap StageMap { get; }
        public Vector2Int MapIndex { get; }

        public UpdateArgument
        (
            StageMap stageMap,
            Vector2Int mapIndex
        )
        {
            StageMap = stageMap;
            MapIndex = mapIndex;
        }
    }
}