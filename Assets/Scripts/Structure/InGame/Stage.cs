using System;
using Module.Option.Runtime;

namespace Structure.InGame
{
    public enum StageStateType
    {
        EntryPoint,
        Normal,
        FloorTransition,
        Stop,
    }

    public enum StageTileType
    {
        None, // ステージ外
        Floor,
        Wall,
        Rubble, // 瓦礫
    }

    public interface IBurnable
    {
        public void SetBurn();
    }
    // ============================================================================================
    // Tile Tip
    // ============================================================================================

    public abstract record StageTileTip;

    public record NoneTileTip : StageTileTip;

    public record FloorTileTip(int InstanceId, int ObjectHealth) : StageTileTip, IBurnable
    {
        public void SetBurn()
        {
            IsBurning = true;
        }
        public bool IsBurning { get; private set; }
    }

    public record WallTileTip(int InstanceId, bool IsBurning, int ObjectHealth) : StageTileTip, IBurnable
    {
        public void SetBurn()
        {
            IsBurning = true;
        }
        public bool IsBurning { get; private set; }
    }

    public record RubbleTileTip(int InstanceId, bool IsBurning, int ObjectHealth) : StageTileTip
    {
        
    }

}