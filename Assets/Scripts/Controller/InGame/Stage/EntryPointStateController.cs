using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.InGame;
using Interface.ViewInterface.InGame;
using Module.StateMachine;
using Structure.InGame;
using Structure.InGame.Stage;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Stage
{
    public class EntryPointStateController : StageStateBehaviour, IStartable
    {
        public EntryPointStateController
        (
            IStageTileMapModel stageTileMapModel,
            IStagePawnModel stagePawnModel,
            IStageTileMapPresenter stageTileMapPresenter,
            IScenePawnsView scenePawnsView,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.EntryPoint, innerState)
        {
            StageTileMapPresenter = stageTileMapPresenter;
            StageTileMapModel = stageTileMapModel;
            StagePawnModel = stagePawnModel;
            ScenePawnsView = scenePawnsView;
        }

        public void Start()
        {
            var map = StageTileMapPresenter.GetMap();
            StageTileMapModel.InitStageMap(map);

            var pawns = ScenePawnsView.GetPawns();
            for (var i = 0; i < pawns.Count; i++)
            {
                var pawnView = pawns[i];
                var gridPosition = StageTileMapPresenter.PositionToMapIndex(pawnView.Floor, pawnView.Position);

                var gridCollider = new GridCollider(
                    pawnView.InstanceId,
                    pawnView.Type,
                    pawnView.Floor,
                    gridPosition,
                    pawnView.Size
                );
                StagePawnModel.StorePawn(gridCollider);
            }

            foreach (var stageMap in StageTileMapModel.StageMaps)
            {
                Debug.Log(stageMap);
            }

            InnerState.ChangeState(StageStateType.Normal);
        }

        private IStageTileMapModel StageTileMapModel { get; }
        private IStagePawnModel StagePawnModel { get; }
        private IStageTileMapPresenter StageTileMapPresenter { get; }
        private IScenePawnsView ScenePawnsView { get; }
    }
}
