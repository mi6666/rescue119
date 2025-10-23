using System;
using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Transform lookAtObject;
        [SerializeField] private float raycastSize = 1f;
        [SerializeField] private float rayCastDistance;
        [SerializeField] private ContactFilter2D rayCastFilter;

        private Transform _selfTransform;
        private Rigidbody2D _rigidbody;
        private RaycastHit2D[] RaycastPool { get; } = new RaycastHit2D[8];

        private void Awake()
        {
            _selfTransform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var selfPosition = _selfTransform.position;

            lookAtObject.position = (Vector2)selfPosition + _rigidbody.velocity;
        }

        public void ApplyVelocity(Vector2 moveTo)
        {
            _rigidbody.velocity = moveTo;
        }

        public Vector2 Position => _selfTransform.position;

        public Vector2 CurrentVelocity => _rigidbody.velocity;

        public ReadOnlySpan<RaycastHit2D> RayCast(Vector2 castTo)
        {
            Vector2 position = _selfTransform!.position;
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