using System;
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
        /// 出口
        /// </summary>
        Exit,

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

    public static class Extension
    {
        /// <summary>
        /// このPawnが壁のように移動の妨げとなるかを記述する
        /// </summary>
        public static bool IsBlock(this PawnType type)
        {
            return type switch
            {
                PawnType.Casualty => false,
                PawnType.Fire => false,
                PawnType.Rubble => true,
                PawnType.Static => true,
                PawnType.Exit => false,
                _ => throw new NotImplementedException("you don't have to arrive here.")
            };
        }

        /// <summary>
        /// このPawnが壁のように移動の妨げとなるかを記述する
        /// </summary>
        public static bool IsHoldable(this PawnType type)
        {
            return type switch
            {
                PawnType.Casualty => true,
                PawnType.Fire => false,
                PawnType.Rubble => true,
                PawnType.Static => false,
                PawnType.Exit => false,
                _ => throw new NotImplementedException("you don't have to arrive here.")
            };
        }
    }
}