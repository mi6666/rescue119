using Module.EditorExtension.Runtime;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RubbleView : BasePawnView
    {
        [SerializeField, AutoAssign] private BoxCollider2D boxCollider2D;
        public override PawnType Type => PawnType.Rubble;
        public override Vector2Int Size => Vector2Int.one;

        public override void OnTake()
        {
            boxCollider2D.enabled = false;
        }

        public override void OnPut()
        {
            boxCollider2D.enabled = true;
        }
    }
}
