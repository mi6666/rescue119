using System;
using R3;
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
}