using System;
using Interface.ModelInterface.InGame;
using Module.Option.Runtime;
using Structure.InGame.Stage;
using UnityEngine;

namespace Model.InGame.Stage
{
    public class TileMapModel : IStageTileMapModel
    {
        public FloorMap[] StageMaps { get; private set; }

        public void InitStageMap(FloorMap[] stageMaps)
        {
            StageMaps = stageMaps;
        }

        public ReadOnlySpan<(Vector2Int, TipBase)> GetAround4Tips(int floor, int x, int y)
        {
            return StageMaps[floor].GetAround4Tips(x, y);
        }

        public Option<TipBase> GetTip(int floor, Vector2Int position)
        {
            return StageMaps[floor].GetTip(position);
        }
    }

    public class EmptyTileMapModel : IStageTileMapModel
    {
        public void InitStageMap(FloorMap[] stageMaps)
        {
        }

        public ReadOnlySpan<(Vector2Int, TipBase)> GetAround4Tips(int floor, int x, int y)
        {
            return Span<(Vector2Int, TipBase)>.Empty;
        }

        public Option<TipBase> GetTip(int floor, Vector2Int position)
        {
            return Option<TipBase>.None();
        }
    }
}