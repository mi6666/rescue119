using System;
using UnityEngine;

namespace Structure.InGame
{
    /// <summary>
    /// プレイヤーの状態を示す
    /// </summary>
    public enum PlayerStateType
    {
        Normal,
        Action,
        Holding,
    }

    public enum PawnType
    {
        /// <summary>
        /// 要救助者
        /// </summary>
        People,

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

    public readonly ref struct LocomotionArgument
    {
        public Vector2 MoveInput { get; }
        public Vector2 CurrentVelocity { get; }
        public ReadOnlySpan<RaycastHit2D> FrontObjects { get; }
        public float DeltaTime { get; }

        public LocomotionArgument
        (
            Vector2 moveInput,
            Vector2 currentVelocity,
            ReadOnlySpan<RaycastHit2D> frontObjects,
            float deltaTime
        )
        {
            MoveInput = moveInput;
            CurrentVelocity = currentVelocity;
            FrontObjects = frontObjects;
            DeltaTime = deltaTime;
        }
    }
}