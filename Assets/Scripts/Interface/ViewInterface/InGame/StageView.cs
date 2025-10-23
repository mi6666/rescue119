using R3;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;

namespace Interface.ViewInterface.InGame
{
    public interface IStageTileMapView
    {
        public StageMap GetMap();
        public Vector2 ConvertToMapPosition(Vector2 position);
    }

    public interface IStageTileView
    {
        public ITileView GetTileView(int instanceId);
    }

    public interface IGimmickEventView
    {
        public Observable<EventContext> GimmickEventObservable { get; }
        public void Invoke(EventContext context);
    }

    public interface ISpawnRubbleView
    {
        public void Spawn(Vector2 position);
    }

    public interface ITileView
    {
        public void ChangeTile(StageTileType stageTileType);
        public void ChangeTileState(TileStateType tileStateType);
    }
}