using System.Collections.Generic;
using Interface.PresenterInterface.InGame;
using Interface.ViewInterface.InGame;
using Structure.InGame;
using Structure.InGame.Stage;

namespace Presenter.InGame.Stage
{
    public class StageMapPresenter : IStageTileMapPresenter
    {
        public StageMapPresenter
        (
            IReadOnlyList<IStageTileMapView> stageTileMapView
        )
        {
            StageTileMapViews = stageTileMapView;
            StageMaps = new StageMap[stageTileMapView.Count];
        }

        public StageMap[] GetMap()
        {
            for (int i = 0; i < StageTileMapViews.Count; i++)
            {
                StageMaps[i] = StageTileMapViews[i].GetMap();
            }

            return StageMaps;
        }

        private StageMap[] StageMaps { get; }
        private IReadOnlyList<IStageTileMapView> StageTileMapViews { get; }
    }
}