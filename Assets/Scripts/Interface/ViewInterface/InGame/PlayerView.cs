using System;
using Module.Option.Runtime;
using Structure.InGame;
using UnityEngine;

namespace Interface.ViewInterface.InGame
{
    public interface IPlayerView
    {
        public Vector2 Position { get; }
        public void ApplyVelocity(Vector2 moveTo);

        public Vector2 CurrentVelocity { get; }
        public ReadOnlySpan<RaycastHit2D> RayCast(Vector2 castTo);
    }

    public interface IPlayerCommandEventView
    {
    }

    public interface IWaterView
    {
        public void SpawnWater();
        public void DespawnWater();
    }

    public interface IDetectPositionView
    {
        public void SetPosition(Vector2 detectionPoint);
        public Vector2 DetectPosition { get; }
    }

    public interface IPawnView
    {
        public PawnType Type { get; }
    }
}