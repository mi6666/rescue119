using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
using Interface.PresenterInterface.InGame;
using Interface.ViewInterface.InGame;
using Module.StateMachine;
using R3;
using Structure.InGame;
using Structure.InGame.Stage;
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
            IGimmickEventView gimmickEventView,
            IStageTileView stageTileView,
            IScenePawnsView scenePawnsView,
            IRubbleFactoryView rubbleFactoryView,
            IStagePawnModel stagePawnModel,
            IStageTileMapModel stageTileMapModel,
            IStageFloorModel stageFloorModel,
            IStageMasterModel stageMasterModel,
            IStageTileMapPresenter stageTileMapPresenter,
            IBurnLogic burnLogic,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.Normal, innerState)
        {
            GimmickEventView = gimmickEventView;
            StageTileView = stageTileView;
            ScenePawnsView = scenePawnsView;
            RubbleFactoryView = rubbleFactoryView;
            StagePawnModel = stagePawnModel;
            StageTileMapModel = stageTileMapModel;
            StageFloorModel = stageFloorModel;
            StageMasterModel = stageMasterModel;
            StageTileMapPresenter = stageTileMapPresenter;
            BurnLogic = burnLogic;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            GimmickEventView.GimmickEventObservable
                .Subscribe(this, (context, controller) => controller.SpawnRubble(context))
                .AddTo(CompositeDisposable);
            // Observable
            //     .Interval(TimeSpan.FromSeconds(StageMasterModel.PawnTickInterval))
            //     .ObserveOnMainThread() // これがないと乱数がきちんと動かない
            //     .Where(this, (_, controller) => controller.IsInState())
            //     .Subscribe(this, (_, controller) => controller.UpdateLogic())
            //     .AddTo(CompositeDisposable);
            Observable.EveryUpdate(UnityFrameProvider.Update)
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
            var pawns = ScenePawnsView.GetPawns();
            var floor = StageFloorModel.CurrentFloor;
            var stageMap = StageTileMapModel.StageMaps[floor];

            for (int i = 0; i < pawns.Count; i++)
            {
                var pawn = pawns[i];
                if (pawn.Floor != floor) continue;

                var position = StageTileMapPresenter.PositionToMapIndex(floor, pawn.Position);
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
                var index = StageTileMapPresenter.PositionToMapIndex(floor, eventContext.SpawnPosition);
                SpawnPawn(index, PawnType.Rubble);
            }
        }

        private void SpawnPawn(Vector2Int mapIndex, PawnType type)
        {
            var floor = StageFloorModel.CurrentFloor;
            var spawnPosition = StageTileMapPresenter.IndexToMapPosition(floor, mapIndex);
            var pawnView = RubbleFactoryView.Spawn(floor, spawnPosition, type);
            var rubbleCollider = new GridCollider(
                pawnView.InstanceId,
                pawnView.Type,
                pawnView.Floor,
                mapIndex,
                pawnView.Size);
            StagePawnModel.StorePawn(rubbleCollider);
            ScenePawnsView.AddPawn(pawnView);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IGimmickEventView GimmickEventView { get; }
        private IScenePawnsView ScenePawnsView { get; }
        private IRubbleFactoryView RubbleFactoryView { get; }
        private IStageTileView StageTileView { get; }
        private IStagePawnModel StagePawnModel { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IStageMasterModel StageMasterModel { get; }
        private IStageTileMapPresenter StageTileMapPresenter { get; }
        private IBurnLogic BurnLogic { get; }
    }
}