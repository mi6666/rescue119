using System;
using Interface.ViewInterface.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
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

        public void ApplyVelocity(Vector2 moveTo)
        {
            _rigidbody.linearVelocity = moveTo;
        }

        public Vector2 CurrentVelocity => _rigidbody.linearVelocity;
        public ReadOnlySpan<RaycastHit2D> RayCast(Vector2 castTo)
        {
            Vector2 position = _selfTransform!.position;
            var hitCount = Physics2D.Raycast
            (
                position,
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