using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.InGame;
using Module.StateMachine;
using Structure.InGame;
using VContainer.Unity;

namespace Controller.InGame.Stage
{
    public class EntryPointStateController : StageStateBehaviour, IStartable
    {
        public EntryPointStateController
        (
            IStageTileMapPresenter stageTileMapPresenter,
            IStageTileMapModel stageTileMapModel,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.EntryPoint, innerState)
        {
            StageTileMapPresenter = stageTileMapPresenter;
            StageTileMapModel = stageTileMapModel;
        }

        public void Start()
        {
            var map = StageTileMapPresenter.GetMap();
            
            StageTileMapModel.InitStageMap(map);
        }

        private IStageTileMapPresenter StageTileMapPresenter { get; }
        private IStageTileMapModel StageTileMapModel { get; }
    }
}