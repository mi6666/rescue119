using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.PresenterInterface.InGame
{
    public interface IStageTileMapPresenter
    {
        public StageMap[] GetMap();
        public Vector2 ToMapPosition(int floor, Vector2 position);
    }
}