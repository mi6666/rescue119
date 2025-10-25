using System;
using System.Collections.Generic;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
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

        public ReadOnlySpan<GridCollider> CastGrid(int floor, Vector2Int position, Vector2Int size, CastTargetType castTargetType)
        {
            if (castTargetType != CastTargetType.Pawn)
            {
                return ReadOnlySpan<GridCollider>.Empty;
            }

            var castArea = new RectInt(position, size);
            var foundColliders = new List<GridCollider>();

            // NOTE: This assumes IStagePawnModel has a "Pawns" property, which needs to be added.
            foreach (var pawnCollider in StagePawnModel.Pawns)
            {
                if (pawnCollider.Floor != floor)
                {
                    continue;
                }

                var pawnArea = new RectInt(pawnCollider.Position, pawnCollider.Size);
                if (pawnArea.Overlaps(castArea))
                {
                    foundColliders.Add(pawnCollider);
                }
            }
            
            // Consider using CollectionsMarshal.AsSpan(foundColliders) if performance is critical
            return new ReadOnlySpan<GridCollider>(foundColliders.ToArray());
        }

        private IStagePawnModel StagePawnModel { get; }
    }
}
