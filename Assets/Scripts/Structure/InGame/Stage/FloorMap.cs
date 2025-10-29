using System;
using System.Text;
using Module.Option.Runtime;
using UnityEngine;

namespace Structure.InGame.Stage
{
    public class FloorMap
    {
        public FloorMap
        (
            TipBase[,] stageTileTips
        )
        {
            StageTileTips = stageTileTips;
            TempBuffer = new (Vector2Int, TipBase)[4];
        }

        public Option<TipBase> GetTip(int x, int y)
        {
            if (x < 0 || x >= LengthX ||
                y < 0 || y >= LengthY)
            {
                return Option<TipBase>.None();
            }

            return Option<TipBase>.Some(StageTileTips[y, x]);
        }

        public ReadOnlySpan<(Vector2Int, TipBase)> GetAround4Tips(int x, int y)
        {
            var getCount = 0;
            foreach (var (dx, dy) in Around4)
            {
                var cursorX = x + dx;
                var cursorY = y + dy;

                var tip = GetTip(cursorX, cursorY);
                if (!tip.TryGetValue(out var tileTip))
                {
                    continue;
                }

                TempBuffer[getCount] = (new Vector2Int(cursorX, cursorY), tileTip);
                getCount++;
            }

            return TempBuffer.AsSpan(0, getCount);
        }

        public int LengthX => StageTileTips.GetLength(1);
        public int LengthY => StageTileTips.GetLength(0);

        private (int, int)[] Around4 { get; } = new[]
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        private (Vector2Int, TipBase)[] TempBuffer { get; }
        private TipBase[,] StageTileTips { get; }

        public override string ToString()
        {
            var builder = new StringBuilder($"(x, y) => ({LengthX.ToString()}, {LengthY.ToString()})\n");

            for (int y = 0; y < LengthY; y++)
            {
                for (int x = 0; x < LengthX; x++)
                {
                    var tip = StageTileTips[y, x];
                    if (tip is not NoneTip)
                    {
                        builder.AppendLine($"position: ({x}, {y}), {tip}");
                    }
                }
            }

            return builder.ToString();
        }
    }
}