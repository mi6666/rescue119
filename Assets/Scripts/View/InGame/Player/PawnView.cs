using Interface.ViewInterface.InGame;
using Module.EditorExtension.Runtime;
using Structure.InGame;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using View.InGame.Stage.Pawn;

namespace View.InGame.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PawnView : BasePawnView, IPawnView
    {
        [SerializeField] private PawnType pawnType;
        [SerializeField] private int floor;
        [SerializeField] private Vector2Int colliderSize;
        [SerializeField, AutoAssign] private BoxCollider2D selfCollider;

        public override PawnType Type => pawnType;
        public override Vector2Int Size => colliderSize;
        public override int Floor => floor;
    }
}