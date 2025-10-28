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
        public Vector2 AlignToMapPosition(Vector2 position);

        public Vector2 IndexToMapPosition(Vector2Int index);

        /// <summary>
        /// マス目のインデックスを求める
        /// </summary>
        public Vector2Int PositionToMapIndex(Vector2 worldPosition);
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

    public interface IRubbleFactoryView
    {
        public IPawnView Spawn(int floor, Vector2 position, PawnType type);
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