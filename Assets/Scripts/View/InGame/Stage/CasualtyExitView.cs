using System;
using Interface.ViewInterface.InGame.UserInterface;
using R3;
using UnityEngine;

namespace View.InGame.Stage
{
    public class CasualtyExitView: MonoBehaviour,IGameClearEventView
    {
        private readonly Subject<Unit> _clearSubject = new();
        public Observable<Unit> GameClearObservable => _clearSubject;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Casualty"))
            {
                _clearSubject.OnNext(Unit.Default);
            }
        }
    }
}