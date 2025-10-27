using System;
using Interface.LogicInterface.InGame;
using Interface.ModelInterface.InGame;
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
            ISpawnRubbleView spawnRubbleView,
            IStageTileView stageTileView,
            IStageTileMapModel stageTileMapModel,
            IStageFloorModel stageFloorModel,
            IBurnLogic burnLogic,
            CompositeDisposable compositeDisposable,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.Normal, innerState)
        {
            GimmickEventView = gimmickEventView;
            SpawnRubbleView = spawnRubbleView;
            StageTileView = stageTileView;
            StageTileMapModel = stageTileMapModel;
            StageFloorModel = stageFloorModel;
            BurnLogic = burnLogic;
            CompositeDisposable = compositeDisposable;
        }

        public void Start()
        {
            GimmickEventView.GimmickEventObservable
                .Subscribe(this, (context, controller) => controller.SpawnRubble(context))
                .AddTo(CompositeDisposable);
            Observable.Interval(TimeSpan.FromSeconds(1.0f))
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (_, controller) => controller.UpdateLogic())
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
                                var tileView= StageTileView.GetTileView(command.ObjectId);

                                tileView.ChangeTileState(TileStateType.Burning);
                            }

                            break;
                    }
                }
            }
        }

        private void SpawnRubble(IEventContext context)
        {
            if (context is SpawnRubbleContext spawnRubbleContext)
            {
                SpawnRubbleView.Spawn(spawnRubbleContext.EventContext.SpawnPosition);
            }
        }

        private CompositeDisposable CompositeDisposable { get; }
        private IGimmickEventView GimmickEventView { get; }
        private ISpawnRubbleView SpawnRubbleView { get; }
        private IStageTileView StageTileView { get; }
        private IStageTileMapModel StageTileMapModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IBurnLogic BurnLogic { get; }
    }
}