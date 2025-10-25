using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using UnityEngine;

namespace View.InGame.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DetectPositionView : MonoBehaviour, IDetectPositionView
    {
        [SerializeField, AutoAssign] private Transform selfTransform;

        public void SetPosition(Vector2 detectionPoint)
        {
            selfTransform.position = detectionPoint;
        }

        public Vector2 DetectPosition => selfTransform.position;
    }
}