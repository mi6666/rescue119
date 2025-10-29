using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
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
            IMapReaderView mapReaderView,
            IFloorPawnView floorPawnView,
            IStageTileMapModel stageTileMapModel,
            IStagePawnModel stagePawnModel,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.EntryPoint, innerState)
        {
            MapReaderView = mapReaderView;
            FloorPawnView = floorPawnView;
            StageTileMapModel = stageTileMapModel;
            StagePawnModel = stagePawnModel;
        }

        public void Start()
        {
            var map = MapReaderView.GetMap();
            StageTileMapModel.InitStageMap(map);

            var pawns = FloorPawnView.GetAllPawn();
            foreach (var pawnView in pawns)
            {
                var gridPosition = MapReaderView.PositionToMapIndex(pawnView.Floor, pawnView.Position);

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

        private IMapReaderView MapReaderView { get; }
        private IFloorPawnView FloorPawnView { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private IStagePawnModel StagePawnModel { get; }
    }
}
