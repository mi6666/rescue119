using Interface.ViewInterface.InGame;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class FirePawnView : BasePawnView, IPawnView
    {
        public override PawnType Type => PawnType.Fire;
        public override Vector2Int Size => Vector2Int.one;
    }
}