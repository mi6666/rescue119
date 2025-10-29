using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.ViewInterface.InGame.Stage
{
    /// <summary>
    /// マップ座標系の変換を行う
    /// </summary>
    public interface IMapCoordinateView
    {
        /// <summary>
        /// マップの升目に座標を揃える
        /// </summary>
        public Vector2 AlignToMapPosition(int floor, Vector2 position);

        /// <summary>
        /// 二次元配列のインデックスから座標に変換する
        /// </summary>
        public Vector2 IndexToMapPosition(int floor, Vector2Int index);

        /// <summary>
        /// 座標から二次元配列のインデックスに変換する
        /// </summary>
        public Vector2Int PositionToMapIndex(int floor, Vector2 worldPosition);
    }

    /// <summary>
    /// マップ読み取りを行う
    /// </summary>
    public interface IMapReaderView : IMapCoordinateView
    {
        /// <summary>
        /// マップ情報を読み取る
        /// </summary>
        public StageMap[] GetMap();
    }

    public interface IStageTileView
    {
        public ITileView GetTileView(int instanceId);
    }

    public interface ITileView
    {
        public void ChangeTile(StageTileType stageTileType);
        public void ChangeTileState(TileStateType tileStateType);
    }
}