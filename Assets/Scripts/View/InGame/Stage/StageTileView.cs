using System.Collections.Generic;
using Interface.ViewInterface.InGame.Stage;
using UnityEngine;

namespace View.InGame.Stage
{
    public class StageTileView : MonoBehaviour, IStageTileView
    {
        private void Start()
        {
            var views = FindObjectsByType<TileTipView>(FindObjectsSortMode.None);

            foreach (var tipView in views)
            {
                TileViews.Add(tipView.gameObject.GetInstanceID(), tipView);
            }
        }

        public ITileView GetTileView(int instanceId)
        {
            return TileViews[instanceId];
        }

        private Dictionary<int, ITileView> TileViews { get; } = new Dictionary<int, ITileView>();
    }
}