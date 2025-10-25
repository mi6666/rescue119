using UnityEngine;

namespace Structure.InGame.Stage
{
    public readonly struct GridCollider
    {
        public int PawnId { get; }
        public Vector2Int Position { get; }
        public Vector2Int Size { get; }

        public GridCollider
        (
            int pawnId,
            Vector2Int position,
            Vector2Int size
        )
        {
            PawnId = pawnId;
            Position = position;
            Size = size;
        }
    }
}