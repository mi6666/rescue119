using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.UserInterface
{
    /// todo
    /// スコア表示
    /// クリアタイム表示
    /// ステージセレクトへ
    /// リスタート
    public class GameClearStateController : UiStateBehaviour
    {
        public GameClearStateController
        (
            IMutStateType<UserInterfaceStateType> innerState
        ) : base(UserInterfaceStateType.GameClear, innerState)
        {
        }
    }
}