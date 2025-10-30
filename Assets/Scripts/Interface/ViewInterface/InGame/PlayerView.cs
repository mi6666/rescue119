using System;
using System.Collections.Generic;
using Structure.InGame.Stage.Pawn;
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

    public interface IPlayerAnimatorView
    {
        public void SetFloat(string key, float value);
    }

    public interface IPlayerCommandEventView
    {
    }

    public interface IWaterView
    {
        public void SpawnWater(Vector2 position, Vector2 lookAt, int length);
        public void DespawnWater();
    }


    public interface IDetectPositionView
    {
        public void SetPosition(Vector2 detectionPoint);
        public Vector2 DetectPosition { get; }
    }

    /// <summary>
    /// シーン上のPawnが持つインターフェース
    /// </summary>
    public interface IPawnView
    {
        public int InstanceId { get; }
        public PawnType Type { get; }
        public Vector2 Position { get; }
        public Vector2Int Size { get; }
        public int Floor { get; }

        public void SetPosition(Vector2 position);
        public void InitFloor(int changedFloor);
        public void SetFloor(Transform newParent, int changedFloor);
    }

    /// <summary>
    /// 動的に配置される`Pawn`のインターフェース
    /// </summary>
    public interface IFactorablePawnView
    {
        public void SetFloor(int newFloor);
    }

    /// <summary>
    /// シーンに存在する全てのPawnを取得するインターフェース
    /// </summary>
    public interface IScenePawnsView
    {
        public IReadOnlyList<IPawnView> GetPawns();
        public IPawnView FindPawn(int id);

        public void AddPawn(IPawnView pawnView);
        public void RemovePawn(int id);
    }
}