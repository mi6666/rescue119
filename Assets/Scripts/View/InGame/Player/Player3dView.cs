using System;
using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.Global;
using UnityEngine;
using ZLinq;

namespace View.InGame.Player
{
    public class Player3dView : MonoBehaviour, IPlayerView
    {
        [SerializeField, AutoAssign] private Transform selfTransform;
        [SerializeField] private Transform raycastPosition;
        [SerializeField] private Transform lookAtObject;
        [SerializeField] private float raycastSize = 1f;
        [SerializeField] private float rayCastDistance;
        [SerializeField] private ContactFilter2D rayCastFilter;

        private Vector2 _prevMove;
        private CastHit[] RaycastPool { get; } = new CastHit[8];
        private RaycastHit[] RaycastHits { get; } = new RaycastHit[8];

        public void ApplyVelocity(Vector2 moveTo)
        {
            _prevMove = moveTo;
            var displacement = new Vector3(moveTo.x, 0, moveTo.y);

            selfTransform.Translate(displacement, Space.World);

            if (displacement != Vector3.zero)
            {
                selfTransform.rotation = Quaternion.LookRotation(displacement);
            }
        }

        public Transform PlayerTransform => selfTransform;
        public Vector2 CurrentVelocity => _prevMove;

        public ReadOnlySpan<CastHit> RayCast(Vector2 castTo)
        {
            var direction = new Vector3(castTo.x, 0, castTo.y);
            Vector2 position = raycastPosition!.position;
            var hitCount = Physics.SphereCastNonAlloc
            (
                position,
                raycastSize,
                direction,
                RaycastHits,
                rayCastDistance,
                rayCastFilter.layerMask
            );

            DebugLogger.Log("direction", direction.ToString());
            var result = RaycastHits.AsValueEnumerable()
                .Select(x => new CastHit(x.normal)).ToArrayPool().Array;
            for (int i = 0; i < hitCount; i++)
            {
                RaycastPool[i] = result[i];
                DebugLogger.Log($"normal{i}, obj: {RaycastHits[i].collider.gameObject.name}",
                    result[i].Normal.ToString());
            }

            return RaycastPool.AsSpan(0, hitCount);
        }
    }
}