using Interface.ModelInterface.InGame;
using Module.StateMachine;
using Structure.InGame;

namespace Controller.InGame.Stage
{
    /// <summary>
    /// <para>タイルの更新処理</para>
    /// <para></para>
    /// </summary>
    public class NormalStateController : StageStateBehaviour
    {
        public NormalStateController
        (
            IStageTileMapModel stageTileMapModel,
            IStageModel stageModel,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.Normal, innerState)
        {
            StageTileMapModel = stageTileMapModel;
            StageModel = stageModel;
        }
        
        
        
        private IStageTileMapModel StageTileMapModel { get; }
        private IStageModel StageModel { get; }
    }
}