using Structure.InGame;
using Structure.InGame.Stage;

namespace Interface.ViewInterface.InGame
{
    public interface IStageTileView
    {
        public StageTileType TileType { get; }
        public int InstanceId { get; }
    }

    public interface IStageTileMapView
    {
        public StageMap GetMap();
    }
}