using System;
using Cysharp.Threading.Tasks;
using Interface.ViewInterface.Global;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
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
            if (!context.performed)
                return;

            // 次のフレームでチェックする
            InvokeLaterAsync().Forget();
        }

        private async UniTaskVoid InvokeLaterAsync()
        {
            await UniTask.NextFrame(); // EventSystemが更新された後
            if (!IsPointerOverUI())
            {
                ActionSubject.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// マウスやタッチがUI上にあるかを判定
        /// </summary>
        private static bool IsPointerOverUI()
        {
            // EventSystemが存在しない場合（たとえばタイトル画面前など）はfalse扱い
            if (EventSystem.current == null)
                return false;

            // マウス操作時
            if (Mouse.current != null)
            {
                return EventSystem.current.IsPointerOverGameObject();
            }

            return false;
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