using Structure.InGame.Stage;

namespace Interface.PresenterInterface.InGame
{
    public interface IStageTileMapPresenter
    {
        public StageMap[] GetMap();

        public int TipGameObject(int objectId);

    }
}