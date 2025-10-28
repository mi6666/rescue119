using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.PresenterInterface.InGame
{
    public interface IStageTileMapPresenter
    {
        public StageMap[] GetMap();

        /// <summary>
        /// 各フロアのマス目に座標を揃える
        /// </summary>
        public Vector2 AlignToMapPosition(int floor, Vector2 position);

        /// <summary>
        /// 各フロアのマス目から座標を出す
        /// </summary>
        public Vector2 IndexToMapPosition(int floor, Vector2Int index);

        /// <summary>
        /// 各フロアのマス目の二次元配列としてインデックスを求める
        /// </summary>
        public Vector2Int PositionToMapIndex(int floor, Vector2 position);
    }
}