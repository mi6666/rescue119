using Interface.ViewInterface.InGame.UserInterface;
using R3;
using Structure.InGame.Stage.Pawn;
using UnityEngine;

namespace View.InGame.Stage.Pawn
{
    public class CasualtyExitView : BasePawnView, IGameClearEventView
    {
        [SerializeField] private int floor;
        [SerializeField] private Vector2Int gridColliderSize;

        private readonly Subject<Unit> _clearSubject = new();
        public Observable<Unit> GameClearObservable => _clearSubject;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Casualty"))
            {
                _clearSubject.OnNext(Unit.Default);
            }
        }

        public override PawnType Type => PawnType.Static;
        public override Vector2Int Size => gridColliderSize;
        public override int Floor => floor;
    }
}