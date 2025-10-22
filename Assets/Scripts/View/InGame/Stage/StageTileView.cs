using System.Collections.Generic;
using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Stage
{
    public class StageTileView : MonoBehaviour, IStageTileView
    {
        private void Awake()
        {
            var views = FindObjectsByType<TileTipView>(FindObjectsSortMode.None);

            foreach (var tipView in views)
            {
                TileViews.Add(tipView.GetInstanceID(), tipView);
            }
        }

        public ITileView GetTileView(int instanceId)
        {
            return TileViews[instanceId];
        }

        private Dictionary<int, ITileView> TileViews { get; } = new Dictionary<int, ITileView>();
    }
}