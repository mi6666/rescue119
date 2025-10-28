using System.Collections.Generic;
using Interface.ViewInterface.InGame;

namespace View.InGame.Stage.Pawn
{
    public class EmptyPawnsView: IScenePawnsView
    {
        public IReadOnlyList<IPawnView> GetPawns()
        {
            return null;
        }

        public IPawnView FindPawn(int id)
        {
            return null;
        }

        public void AddPawn(IPawnView pawnView)
        {
        }

        public void RemovePawn(int id)
        {
        }
    }
}