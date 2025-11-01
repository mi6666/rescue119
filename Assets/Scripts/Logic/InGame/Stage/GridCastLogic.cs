using System;
using System.Runtime.CompilerServices;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Module.Option.Runtime;
using Structure.InGame.Stage;
using UnityEngine;
using VContainer;

namespace Logic.InGame.Stage
{
    public class GridCastLogic : IGridCastLogic
    {
        [Inject]
        public GridCastLogic
        (
            IStagePawnModel stagePawnModel
        )
        {
            StagePawnModel = stagePawnModel;
        }

        public ReadOnlySpan<GridCollider> CastGrid
        (
            int floor,
            Vector2Int position,
            Vector2Int size,
            CastTargetType castTargetType
        )
        {
            Debug.Assert(castTargetType == CastTargetType.Pawn, "Not Implemented");

            var count = 0;
            var castArea = new RectInt(position, size);

            foreach (var pawnCollider in StagePawnModel.Pawns)
            {
                var isOverlap = InnerCast(floor, castArea, pawnCollider);
                if (isOverlap)
                {
                    FoundColliders[count] = pawnCollider;
                    count++;
                }
            }

            return FoundColliders.AsSpan(0, count);
        }

        public Option<GridCollider> CastGridFirst(int floor, Vector2Int position, Vector2Int size, CastTargetType castTarget)
        {
            Debug.Assert(castTarget == CastTargetType.Pawn, "Not Implemented");

            var castArea = new RectInt(position, size);

            foreach (var pawnCollider in StagePawnModel.Pawns)
            {
                var isOverlap = InnerCast(floor, castArea, pawnCollider);
                if (isOverlap)
                {
                    return Option<GridCollider>.Some(pawnCollider);
                }
            }

            return Option<GridCollider>.None();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool InnerCast(int floor, RectInt castArea, GridCollider other)
        {
            if (other.Floor != floor)
            {
                return false;
            }

            var pawnArea = new RectInt(other.Position, other.Size);
            return pawnArea.Overlaps(castArea);
        }

        private GridCollider[] FoundColliders { get; } = new GridCollider[8];
        private IStagePawnModel StagePawnModel { get; }
    }
}