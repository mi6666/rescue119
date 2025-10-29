using Structure.InGame.Stage.Pawn;
using UnityEngine;
using UnityEngine.Pool;

namespace Interface.ViewInterface.InGame.Stage
{
    /// <summary>
    /// フロア全体の表示・非表示を行う
    /// </summary>
    public interface IFloorView
    {
        public void Activate();
        public void Deactivate();
    }

    /// <summary>
    /// `Pawn`についてフロアを横断した管理を行う
    /// </summary>
    public interface IPawnPoolView  // FIXME?
    {
        /// <summary>
        /// フロアでの`Pawn`の所有権を移動させる
        /// </summary>
        public IPawnView Move(int id, int floorPrevious, int floorNext);

        /// <summary>
        /// 新しい`Pawn`をプールから取得する
        /// </summary>
        public IPawnView Spawn(int floor, Vector2 position, PawnType type);

        /// <summary>
        /// `Pawn`をプールに返す
        /// </summary>
        public void Despawn(int id, int floorPrevious);
    }

    public interface IPawnPoolable<T> where T : class
    {
        public void SetPool(IObjectPool<T> objectPool);

        public void Spawn(int floor);
    }
}