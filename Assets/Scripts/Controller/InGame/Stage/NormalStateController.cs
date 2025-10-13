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
    public class NormalStateController : StageStateBehaviour,IStartable
    {
        public NormalStateController
        (
            IStageTileMapModel stageTileMapModel,
            IStageFloorModel stageFloorModel,
            IBurnLogic burnLogic,
            IGimmickEventView gimmickEventView,
            CompositeDisposable compositeDisposable,
            ISpawnRubbleView spawnRubbleView,
            IMutStateType<StageStateType> innerState
        ) : base(StageStateType.Normal, innerState)
        {
            StageTileMapModel = stageTileMapModel;
            StageFloorModel = stageFloorModel;
            BurnLogic = burnLogic;
            GimmickEventView = gimmickEventView;
            CompositeDisposable = compositeDisposable;
            SpawnRubbleView = spawnRubbleView;
        }
        
        public void Start()
        {
            GimmickEventView.GimmickEventObservable
                .Subscribe(this, (context, controller) => controller.SpawnRubble(context))
                .AddTo(CompositeDisposable);
        }

        public override void StateUpdate(float deltaTime)
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
                        new Vector2Int(x, y),
                        deltaTime
                    );

                    switch (tip)
                    {
                        case ITipBurnable tipBurnable:
                            BurnLogic.Update(tipBurnable, arg);
                            break;
                    }
                }
            }
        }

        private void SpawnRubble(EventContext context)
        {
            SpawnRubbleView.Spawn(context.SpawnPosition);
        }

        private IStageTileMapModel StageTileMapModel { get; }
        private IStageFloorModel StageFloorModel { get; }
        private IBurnLogic BurnLogic { get; }
        private IGimmickEventView GimmickEventView { get; }
        private CompositeDisposable CompositeDisposable { get; }
        private ISpawnRubbleView SpawnRubbleView { get; }
    }
}