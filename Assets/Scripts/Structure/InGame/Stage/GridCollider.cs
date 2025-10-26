using System.Text;
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

        public override string ToString()
        {
            var builder = new StringBuilder(nameof(GridCollider));
            builder.Append("{");

            Append(nameof(PawnId), PawnId.ToString());
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