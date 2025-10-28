using Interface.ModelInterface.InGame;
using Structure.InGame.Stage;

namespace Model.InGame.Stage
{
    public class TileMapModel: IStageTileMapModel
    {
        public StageMap[] StageMaps { get; private set; }
        public void InitStageMap(StageMap[] stageMaps)
        {
            StageMaps = stageMaps;
        }
    }
}