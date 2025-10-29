using Interface.ViewInterface.InGame;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class FirePawnView : BasePawnView, IPawnView, IFactorablePawnView
    {
        [SerializeField] private int floor;

        public override PawnType Type => PawnType.Fire;
        public override Vector2Int Size => Vector2Int.one;
        public override int Floor => floor;
        
        public void SetFloor(int newFloor)
        {
            floor = newFloor;
        }
    }
}