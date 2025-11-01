using System;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.Global;
using UnityEngine;
using ZLinq;

namespace View.InGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField, AutoAssign] private Transform selfTransform;
        [SerializeField] private Transform lookAtObject;
        [SerializeField] private float raycastSize = 1f;
        [SerializeField] private float rayCastDistance;
        [SerializeField] private ContactFilter2D rayCastFilter;

        private Vector2 _prevMove;
        private CastHit[] RaycastPool { get; } = new CastHit[8];
        private RaycastHit2D[] RaycastHits { get; } = new RaycastHit2D[8];

        public void ApplyVelocity(Vector2 moveTo)
        {
            _prevMove = moveTo;
            selfTransform.Translate(moveTo);
        }

        public Transform PlayerTransform => selfTransform;
        public Vector2 CurrentVelocity => _prevMove;

        public ReadOnlySpan<CastHit> RayCast(Vector2 castTo)
        {
            Vector2 position = selfTransform!.position;
            var hitCount = Physics2D.CircleCast
            (
                position,
                raycastSize,
                castTo,
                rayCastFilter,
                RaycastHits,
                rayCastDistance
            );
            
            var result = RaycastHits.AsValueEnumerable()
                .Select(x => new CastHit(x.normal)).ToArrayPool().Array;
            for (int i = 0; i < hitCount; i++)
            {
                RaycastPool[i] = result[i];
            }

            var castResult = RaycastPool.AsSpan(0, hitCount);

            return castResult;
        }
    }
}