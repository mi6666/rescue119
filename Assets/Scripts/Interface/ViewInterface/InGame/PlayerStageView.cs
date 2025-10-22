using System;
using Module.Option.Runtime;
using Structure.InGame;
using UnityEngine;

namespace Interface.ViewInterface.InGame
{
    public interface IPlayerView
    {
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

    public interface IPawnDetectView
    {
        public Option<PawnType> Detection();
    }

    public interface IPawnView
    {
        public PawnType Type { get; }
    }
}