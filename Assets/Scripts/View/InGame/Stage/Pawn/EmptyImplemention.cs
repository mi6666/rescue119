using System.Collections.Generic;
using Interface.ViewInterface.InGame;
using R3;
using Structure.InGame.Stage;

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

    public class EmptyStairEventView: IStairEventView
    {
        private Subject<IEventContext> InnerSubject { get; } = new ();
        public Observable<IEventContext> GimmickEventObservable => InnerSubject;
        public void Invoke(IEventContext context)
        {
            
        }
    }
}