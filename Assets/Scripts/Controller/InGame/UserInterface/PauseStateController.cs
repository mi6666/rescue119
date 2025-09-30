using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.UserInterface
{
    /// todo
    /// リタイア
    /// ポーズ終了
    public class PauseStateController : UiStateBehaviour
    {
        public PauseStateController
        (
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.Pause, innerState)
        {
        }
    }
}