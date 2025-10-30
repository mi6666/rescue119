using System;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using UnityEngine;

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
        private RaycastHit2D[] RaycastPool { get; } = new RaycastHit2D[8];

        public void ApplyVelocity(Vector2 moveTo)
        {
            _prevMove = moveTo;
            selfTransform.Translate(moveTo);
        }

        public Transform PlayerTransform => selfTransform;
        public Vector2 CurrentVelocity => _prevMove;

        public ReadOnlySpan<RaycastHit2D> RayCast(Vector2 castTo)
        {
            Vector2 position = selfTransform!.position;
            var hitCount = Physics2D.CircleCast
            (
                position,
                raycastSize,
                castTo,
                rayCastFilter,
                RaycastPool,
                rayCastDistance
            );
            var castResult = RaycastPool.AsSpan(0, hitCount);

            return castResult;
        }
    }
}