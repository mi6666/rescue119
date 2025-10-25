using Interface.ModelInterface.InGame;
using Module.Option.Runtime;
using Structure.InGame.Stage;
using UnityEngine;
using System.Collections.Generic;

namespace Model.InGame.Stage
{
    /// <summary>
    /// todo
    /// オブジェクトプールのような出来る限りアロケーションを起こさない仕組みで
    /// Gridの当たり判定を管理する
    /// </summary>
    public class StagePawnModel: IStagePawnModel
    {
        private readonly Dictionary<int, GridCollider> _pawns = new();

        public void StorePawn(GridCollider gridCollider)
        {
            _pawns[gridCollider.PawnId] = gridCollider;
        }

        public void RemovePawn(int id)
        {
            _pawns.Remove(id);
        }


        public Option<int> CastPosition(Vector2Int position)
        {
            foreach (var pawn in _pawns.Values)
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
