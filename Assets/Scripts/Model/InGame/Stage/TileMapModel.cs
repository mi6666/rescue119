using Interface.ModelInterface.InGame;
using Structure.InGame.Stage;

namespace Model.InGame.Stage
{
    public class TileMapModel: IStageTileMapModel
    {
        public FloorMap[] StageMaps { get; private set; }
        public void InitStageMap(FloorMap[] stageMaps)
        {
            StageMaps = stageMaps;
        }
    }
}