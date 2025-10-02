using System;
using Module.Option.Runtime;

namespace Structure.InGame
{
    public enum StageStateType
    {
        Normal,
        Stop,
    }

    public enum StageTileType
    {
        None, // ステージ外
        Floor,
        Wall,
        Rubble, // 瓦礫
    }

    // ============================================================================================
    // Tile Tip
    // ============================================================================================

    public abstract record StageTileTip;

    public record NoneTileTip : StageTileTip;

    public record FloorTileTip(int InstanceId, bool IsBurning, int ObjectHealth) : StageTileTip;

    public record WallTileTip(int InstanceId, bool IsBurning, int ObjectHealth) : StageTileTip;

    public record RubbleTileTip(int InstanceId, bool IsBurning, int ObjectHealth) : StageTileTip;

    public class StageMap
    {
        public StageMap
        (
            StageTileTip[,] stageTileTips
        )
        {
            StageTileTips = stageTileTips;
            TempBuffer = new StageTileTip[4];
        }

        public Option<StageTileTip> GetTip(int x, int y)
        {
            if (x < 0 || x >= StageTileTips.GetLength(0) ||
                y < 0 || y >= StageTileTips.GetLength(1))
            {
                return Option<StageTileTip>.None();
            }

            return Option<StageTileTip>.Some(StageTileTips[x, y]);
        }

        public ReadOnlySpan<StageTileTip> GetAroundTips(int x, int y)
        {
            var getCount = 0;
            foreach (var (dx, dy) in Around)
            {
                var cursorX = x + dx;
                var cursorY = y + dy;

                var tip = GetTip(cursorX, cursorY);
                if (!tip.TryGetValue(out var tileTip))
                {
                    continue;
                }

                TempBuffer[getCount] = tileTip;
                getCount++;
            }

            return TempBuffer.AsSpan(0, getCount);
        }

        private (int, int)[] Around { get; } = new[]
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        private StageTileTip[] TempBuffer { get; }
        private StageTileTip[,] StageTileTips { get; }
    }
}