using UnityEngine;

namespace Structure.InGame.Stage
{
    public readonly struct GridCollider
    {
        public int PawnId { get; }
        public int Floor { get; }
        public Vector2Int Position { get; }
        public Vector2Int Size { get; }

        public GridCollider
        (
            int pawnId,
            int floor,
            Vector2Int position,
            Vector2Int size
        )
        {
            PawnId = pawnId;
            Floor = floor;
            Position = position;
            Size = size;
        }
    }
}