using Structure.InGame;

namespace Interface.ViewInterface.InGame
{
    public interface IStageTileView
    {
        public StageTileType TileType { get; }
        public int InstanceId { get; }
    }

    public readonly struct StageTileInfo
    {
        
    }
}