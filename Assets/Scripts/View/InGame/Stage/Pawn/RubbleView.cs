using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class RubbleView : BasePawnView
    {
        public override PawnType Type => PawnType.Rubble;
        public override Vector2Int Size => Vector2Int.one;
    }
}
