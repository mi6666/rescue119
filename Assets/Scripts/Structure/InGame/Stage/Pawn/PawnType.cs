using UnityEngine;

namespace Structure.InGame.Stage.Pawn
{
    /// <summary>
    /// ポーン: ステージ上のオブジェクトとして存在するマップチップではないもの。
    /// `PawnType`はその中でも、移動・増加しうるものを判別する。
    /// </summary>
    public enum PawnType
    {
        /// <summary>
        /// 負傷者
        /// </summary>
        Casualty,

        /// <summary>
        /// 炎
        /// </summary>
        Fire,

        /// <summary>
        /// 瓦礫
        /// </summary>
        Rubble,

        /// <summary>
        /// 特に動かないオブジェクト
        /// </summary>
        Static,
    }

    /// <summary>
    /// ポーンの情報についてModelで管理される個々の実体を指す
    /// </summary>
    public readonly struct PawnEntity
    {
        public int Id { get; }
        public PawnType Type { get; }
        public int Floor { get; }
        public Vector2Int Position { get; }
        public Vector2Int Size { get; }

        public GridCollider Collider => new GridCollider(
            Id, Type, Floor, Position, Size
        );
    }
}