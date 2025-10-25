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
        public Vector2 ToMapPosition(int floor, Vector2 position);

        /// <summary>
        /// 各フロアのマス目の二次元配列としてインデックスを求める
        /// </summary>
        public Vector2Int ToMapIndex(int floor, Vector2 position);
    }
}