using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using UnityEngine;

namespace View.InGame.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PawnView : MonoBehaviour, IPawnView
    {
        [SerializeField] private PawnType pawnType;
        [SerializeField] private int floor;
        [SerializeField] private Vector2Int colliderSize;
        [SerializeField, AutoAssign] private BoxCollider2D selfCollider;

        public int InstanceId => gameObject.GetInstanceID();
        public PawnType Type => pawnType;
        public Vector2 Position => transform.position;
        public Vector2Int Size => colliderSize;
        public int Floor => floor;
    }
}