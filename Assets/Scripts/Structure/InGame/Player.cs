using System;
using Structure.Global;
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
        Stopping,
    }

    public readonly ref struct LocomotionArgument
    {
        public Vector2 MoveInput { get; }
        public Vector2 CurrentVelocity { get; }
        public ReadOnlySpan<CastHit> FrontObjects { get; }
        public float DeltaTime { get; }

        public LocomotionArgument
        (
            Vector2 moveInput,
            Vector2 currentVelocity,
            ReadOnlySpan<CastHit> frontObjects,
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