using R3;
using UnityEngine;

namespace Interface.ViewInterface.Global
{
    public interface IInput_MoveVectorView
    {
        public Vector2 Pool();
    }

    public interface IInput_ActionEventView
    {
        public Observable<Unit> ActionObservable { get; }
    }
}