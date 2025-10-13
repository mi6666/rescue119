using System;
using Interface.ViewInterface.Global;
using UnityEngine;

namespace View.Global.Input
{
    public class InputWrapper : IInput_MoveVectorView, IDisposable
    {
        public InputWrapper(InputSystem_Actions inputSystemActions)
        {
            InputSystemActions = inputSystemActions;
            
            InputSystemActions.Player.Enable();
        }

        public Vector2 Pool()
        {
            return InputSystemActions.Player.Move.ReadValue<Vector2>();
        }

        private InputSystem_Actions InputSystemActions { get; }

        public void Dispose()
        {
            InputSystemActions.Disable();
            InputSystemActions?.Dispose();
        }
    }
}