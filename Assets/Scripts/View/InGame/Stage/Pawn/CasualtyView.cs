using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class CasualtyView: BasePawnView
    {
        public override PawnType Type => PawnType.Casualty;
        public override Vector2Int Size => Vector2Int.one;
    }
}