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
            IFloorView floorView,
            IStageTileMapModel stageTileMapModel,
            IStagePawnModel stagePawnModel,
            IStageFloorModel stageFloorModel,
            IStageMasterModel stageMasterModel,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.EntryPoint, innerState)
        {
            MapReaderView = mapReaderView;
            FloorPawnView = floorPawnView;
            FloorView = floorView;
            StageTileMapModel = stageTileMapModel;
            StagePawnModel = stagePawnModel;
            StageFloorModel = stageFloorModel;
            StageMasterModel = stageMasterModel;
        }

        public void Start()
        {
            // マップ情報の初期化
            var map = MapReaderView.GetMap();
            StageTileMapModel.InitStageMap(map);

            // ポーン情報の初期化
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

            // フロアの表示を初期化
            for (int i = 0; i < StageMasterModel.MaxFloorNum; i++)
            {
                Debug.Log($"{i}: {StageFloorModel.CurrentFloor.ToString()}");
                if (i == StageFloorModel.CurrentFloor)
                {
                    FloorView.Activate(i);
                }
                else
                {
                    FloorView.Deactivate(i);
                }
            }

            InnerState.ChangeState(StageStateType.Normal);
        }

        private IMapReaderView MapReaderView { get; }
        private IFloorPawnView FloorPawnView { get; }
        private IFloorView FloorView { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private IStagePawnModel StagePawnModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IStageMasterModel StageMasterModel { get; }
    }
}
