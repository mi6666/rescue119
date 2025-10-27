using R3;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;
using UnityEngine.Pool;

namespace Interface.ViewInterface.InGame
{
    public interface IStageTileMapView
    {
        public StageMap GetMap();

        /// <summary>
        /// マス目に座標を揃える
        /// </summary>
        public Vector2 ConvertToMapPosition(Vector2 position);

        /// <summary>
        /// マス目のインデックスを求める
        /// </summary>
        public Vector2Int WorldToCell(Vector2 worldPosition);
    }

    public interface IStageTileView
    {
        public ITileView GetTileView(int instanceId);
    }

    public interface IGimmickEventView
    {
        public Observable<IEventContext> GimmickEventObservable { get; }
        public void Invoke(IEventContext context);
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

    public interface IStairEventView : IGimmickEventView
    {
        public Observable<StairType> StairsEventObservable => GimmickEventObservable
            .Where(x => x is StairContext)
            .Select(x => (x as StairContext)!.EventContext.StairType);
    }

    public interface IPawnPoolable<T> where T : class
    {
        public void SetPool(IObjectPool<T> objectPool);

        public void Spawn(int floor);
    }
}