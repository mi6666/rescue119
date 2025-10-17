using System;
using Interface.ViewInterface.Global;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View.Global.Input
{
    public class InputWrapper : IInput_MoveVectorView, IInput_ActionEventView, IDisposable
    {
        public InputWrapper(InputSystem_Actions inputSystemActions)
        {
            InputSystemActions = inputSystemActions;

            InputSystemActions.Player.Enable();
            InputSystemActions.Player.Attack.performed += InvokeAction;
        }

        public Vector2 Pool()
        {
            return InputSystemActions.Player.Move.ReadValue<Vector2>();
        }

        private void InvokeAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ActionSubject.OnNext(Unit.Default);
            }
        }

        public Observable<Unit> ActionObservable => ActionSubject;

        private InputSystem_Actions InputSystemActions { get; }
        private Subject<Unit> ActionSubject { get; } = new();

        public void Dispose()
        {
            ActionSubject.Dispose();
            InputSystemActions.Player.Attack.performed -= InvokeAction;
            InputSystemActions.Disable();
            InputSystemActions?.Dispose();
        }
    }
}