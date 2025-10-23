using System.Collections.Generic;
using Interface.PresenterInterface.InGame;
using Interface.ViewInterface.InGame;
using Structure.InGame.Stage;
using UnityEngine;

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
                var map = StageTileMapViews[i].GetMap();
                StageMaps[i] = map;
            }

            return StageMaps;
        }

        public Vector2 ToMapPosition(int floor, Vector2 position)
        {
            return StageTileMapViews[floor].ConvertToMapPosition(position);
        }

        private StageMap[] StageMaps { get; }
        private IReadOnlyList<IStageTileMapView> StageTileMapViews { get; }
    }
}