using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Structure.Global;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace Interface.ViewInterface.InGame
{
    public interface IPlayerView
    {
        public Transform PlayerTransform { get; }
        public Vector2 Position => PlayerTransform.position;
        public void ApplyVelocity(Vector2 moveTo);

        public Vector2 CurrentVelocity { get; }
        public ReadOnlySpan<CastHit> RayCast(Vector2 castTo);
    }

    public interface IPlayerAnimatorView
    {
        public void SetFloat(string key, float value);
    }

    public interface IWaterView
    {
        public void SpawnWater(Vector2 position, Vector2 lookAt, int length);
        public void DespawnWater();
    }

    public interface IHoldingPawnView
    {
        public UniTask HoldPawn(IPawnView pawnView);
        public UniTask PutPawn(Vector2 position);
        public UniTask PutAndFall(Vector2 position);
        public IPawnView HoldingPawn { get; }
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

        public Transform PawnTransform { get; }

        public void InitFloor(int changedFloor);
        public void SetFloor(Transform newParent, int changedFloor);

        /// <summary>
        /// `Pawn`が置かれた際に発動する
        /// </summary>
        public void OnPut();

        /// <summary>
        /// `Pawn`が取り除かれた際に発動する
        /// </summary>
        public void OnTake();
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