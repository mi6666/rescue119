using System;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.ViewInterface.InGame.Stage;
using Module.StateMachine;
using R3;
using Structure.InGame;
using Structure.InGame.Stage;
using Structure.InGame.Stage.Pawn;
using UnityEngine;
using VContainer.Unity;

namespace Controller.InGame.Stage
{
    /// <summary>
    /// <para>タイルの更新処理</para>
    /// <para></para>
    /// </summary>
    public class NormalStateController : StageStateBehaviour, IStartable
    {
        public NormalStateController
        (
            IPawnEventView pawnEventView,
            IStageTileView stageTileView,
            IFloorPawnView  floorPawnView,
            IMapCoordinateView mapCoordinateView,
            IPawnPoolView pawnPoolView,
            IStagePawnModel stagePawnModel,
            IStageTileMapModel stageTileMapModel,
            IStageFloorModel stageFloorModel,
            IStageMasterModel stageMasterModel,
            IBurnLogic burnLogic,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.Normal, innerState)
        {
            PawnEventView = pawnEventView;
            StageTileView = stageTileView;
            FloorPawnView = floorPawnView;
            MapCoordinateView = mapCoordinateView;
            PawnPoolView = pawnPoolView;
            StagePawnModel = stagePawnModel;
            StageTileMapModel = stageTileMapModel;
            StageFloorModel = stageFloorModel;
            StageMasterModel = stageMasterModel;
            BurnLogic = burnLogic;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            PawnEventView.GimmickEventObservable
                .Subscribe(this, (context, controller) => controller.SpawnRubble(context))
                .AddTo(CompositeDisposable);
            Observable
                .Interval(TimeSpan.FromSeconds(StageMasterModel.PawnTickInterval))
                .ObserveOnMainThread() // これがないと乱数がきちんと動かない
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (_, controller) => controller.UpdatePawnLogic())
                .AddTo(CompositeDisposable);
        }

        private void UpdateLogic()
        {
            if (StageTileMapModel.StageMaps is null) return;

            var stageMap = StageTileMapModel.StageMaps[StageFloorModel.CurrentFloor];

            for (var y = 0; y < stageMap.LengthY; y++)
            {
                for (var x = 0; x < stageMap.LengthX; x++)
                {
                    if (!stageMap.GetTip(x, y).TryGetValue(out var tip)) continue;
                    var arg = new UpdateArgument(
                        stageMap,
                        new Vector2Int(x, y)
                    );

                    switch (tip)
                    {
                        case ITipBurnable tipBurnable:
                            var result = BurnLogic.Update(tipBurnable, arg);
                            foreach (var command in result)
                            {
                                var tileView = StageTileView.GetTileView(command.ObjectId);

                                tileView.ChangeTileState(TileStateType.Burning);
                            }

                            break;
                    }
                }
            }
        }

        private void UpdatePawnLogic()
        {
            var floor = StageFloorModel.CurrentFloor;
            var stageMap = StageTileMapModel.StageMaps[floor];
            var pawns = FloorPawnView.GetFloorPawn(floor);

            for (int i = 0; i < pawns.Length; i++)
            {
                var pawn = pawns[i];
                if (pawn.Floor != floor) continue;

                var position = MapCoordinateView.PositionToMapIndex(floor, pawn.Position);
                var arg = new UpdateArgument(stageMap, new Vector2Int(position.x, position.y));

                var result = BurnLogic.Update(floor, arg);
                foreach (var command in result)
                {
                    SpawnPawn(command.MapIndex, command.Type);
                }
            }
        }

        private void SpawnRubble(IEventContext context)
        {
            if (context is SpawnRubbleContext spawnRubbleContext)
            {
                var eventContext = spawnRubbleContext.EventContext;
                var floor = StageFloorModel.CurrentFloor;
                var index = MapCoordinateView.PositionToMapIndex(floor, eventContext.SpawnPosition);
                SpawnPawn(index, PawnType.Rubble);
            }
        }

        private void SpawnPawn(Vector2Int mapIndex, PawnType type)
        {
            var floor = StageFloorModel.CurrentFloor;
            var spawnPosition = MapCoordinateView.IndexToMapPosition(floor, mapIndex);
            var pawnView = PawnPoolView.Spawn(spawnPosition, type);
            var rubbleCollider = new GridCollider(
                pawnView.InstanceId,
                pawnView.Type,
                pawnView.Floor,
                mapIndex,
                pawnView.Size);
            StagePawnModel.StorePawn(rubbleCollider);
            FloorPawnView.GivePawn(pawnView, floor);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IPawnEventView PawnEventView { get; }
        private IFloorPawnView FloorPawnView { get; }
        private IPawnPoolView PawnPoolView { get; }
        private IMapCoordinateView MapCoordinateView { get; }
        private IStageTileView StageTileView { get; }
        private IStagePawnModel StagePawnModel { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IStageMasterModel StageMasterModel { get; }
        private IBurnLogic BurnLogic { get; }
    }
}