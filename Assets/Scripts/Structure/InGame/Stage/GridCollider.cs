using System.Text;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace Structure.InGame.Stage
{
    public readonly struct GridCollider
    {
        public int PawnId { get; }
        public PawnType PawnType { get; }
        public int Floor { get; }
        public Vector2Int Position { get; }
        public Vector2Int Size { get; }

        public GridCollider
        (
            int pawnId,
            PawnType pawnType,
            int floor,
            Vector2Int position,
            Vector2Int size
        )
        {
            PawnId = pawnId;
            PawnType = pawnType;
            Floor = floor;
            Position = position;
            Size = size;
        }

        public override string ToString()
        {
            var builder = new StringBuilder(nameof(GridCollider));
            builder.Append("{");

            Append(nameof(PawnId), PawnId.ToString());
            Append(nameof(PawnType), PawnType.ToString());
            Append(nameof(Floor), Floor.ToString());
            Append(nameof(Position), Position.ToString());
            Append(nameof(Size), Size.ToString());

            builder.Append("}");

            return builder.ToString();

            void Append(string name, string value)
            {
                builder.Append(name);
                builder.Append(": ");
                builder.AppendLine(value);
            }
        }
    }
}