using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.Primary
{
    /// <summary>
    /// 状態にとらわれず行う処理
    ///
    /// ゲームオーバーへの遷移
    /// </summary>
    public class AnyStateController
    {
        public AnyStateController
        (
            IMutStateType<PrimaryStateType> stateType
        )
        {
            MutStateType = stateType;
        }
        
        private IMutStateType<PrimaryStateType> MutStateType { get; }
    }
}