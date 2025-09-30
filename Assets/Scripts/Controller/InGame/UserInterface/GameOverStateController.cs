using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.UserInterface
{
    /// todo
    /// リタイア
    /// リスタート
    public class GameOverStateController : UiStateBehaviour
    {
        public GameOverStateController
        (
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.GameOver, innerState)
        {
        }
    }
}