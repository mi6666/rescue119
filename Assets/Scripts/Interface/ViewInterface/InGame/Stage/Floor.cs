using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace Interface.ViewInterface.InGame.Stage
{
    /// <summary>
    /// フロア全体の表示・非表示を行う
    /// </summary>
    public interface IFloorView
    {
        /// <summary>
        /// フロア移動として前回フロアを非表示化し、次のフロアを表示する。
        /// </summary>
        public void MoveFloor(int prevFloor, int nextFloor)
        {
            Deactivate(prevFloor);
            Activate(nextFloor);
        }

        public void Activate(int floor);
        public void Deactivate(int floor);
    }

    /// <summary>
    /// 全フロアの`Pawn`を管理する
    /// </summary>
    public interface IFloorPawnView
    {
        /// <summary>
        /// すべての`Pawn`を取得する。
        /// </summary>
        public IPawnView[] GetAllPawn();
        
        /// <summary>
        /// フロア中すべての`Pawn`を取得する。
        /// </summary>
        public IPawnView[] GetFloorPawn(int floor);
        
        /// <summary>
        /// `Pawn`を別フロアへ移動させる。
        /// </summary>
        public IPawnView MovePawn(int id, int floorPrevious, int floorNext);

        /// <summary>
        /// `Pawn`のViewを取得する。
        /// </summary>
        public IPawnView GetPawn(int id, int floor);

        /// <summary>
        /// フロアの`Pawn`を所有権ごと取得する。
        /// (フロアに存在しているIPawnViewへの参照を断ち切り、取得する)
        /// </summary>
        public IPawnView TakePawn(int id, int floor);

        /// <summary>
        /// フロアに`Pawn`を与える。
        /// </summary>
        public void GivePawn(IPawnView pawnView, int floor);
    }

    /// <summary>
    /// `Pawn`のプールを管理する
    /// </summary>
    public interface IPawnPoolView
    {
        /// <summary>
        /// 新しい`Pawn`をプールから取得する
        /// </summary>
        public IPawnView Spawn(Vector2 position, PawnType type);

        /// <summary>
        /// `Pawn`をプールに返す
        /// </summary>
        public void Despawn(IPawnView pawnView);
    }
}