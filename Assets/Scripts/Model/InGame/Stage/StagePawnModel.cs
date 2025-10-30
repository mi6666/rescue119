using System;
using Interface.ModelInterface.InGame;
using Module.Option.Runtime;
using Structure.InGame.Stage;
using UnityEngine;
using ZLinq;

namespace Model.InGame.Stage
{
    public class StagePawnModel : IStagePawnModel
    {
        private GridCollider[] PawnArray { get; } = new GridCollider[256];
        private int _length;

        public ReadOnlySpan<GridCollider> Pawns => PawnArray.AsSpan(0, _length);

        public void StorePawn(GridCollider gridCollider)
        {
            // if (PawnArray.AsValueEnumerable().Any(x => x.Position == gridCollider.Position))
            // {
            //     Debug.Log($"length: {_length.ToString()}, inserted: {gridCollider.ToString()}");
            //     for (int i = 0; i < _length; i++)
            //     {
            //         Debug.Log(PawnArray[i]);
            //     }
            // }
            Debug.Assert(_length < PawnArray.Length, "Pawn array is full.");
            PawnArray[_length] = gridCollider;
            _length++;
        }

        public void RemovePawn(int id)
        {
            int index = -1;
            for (int i = 0; i < _length; i++)
            {
                if (PawnArray[i].PawnId == id)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                _length--;
                PawnArray[index] = PawnArray[_length];
            }
        }


        public Option<int> CastPosition(Vector2Int position)
        {
            foreach (var pawn in Pawns)
            {
                var pawnPosition = pawn.Position;
                var pawnSize = pawn.Size;

                var minX = pawnPosition.x;
                var maxX = pawnPosition.x + pawnSize.x;
                var minY = pawnPosition.y;
                var maxY = pawnPosition.y + pawnSize.y;

                if (position.x >= minX && position.x < maxX &&
                    position.y >= minY && position.y < maxY)
                {
                    return Option<int>.Some(pawn.PawnId);
                }
            }

            return Option<int>.None();
        }
    }
}