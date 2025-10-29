using Controller.InGame.Stage;
using Logic.InGame.Stage;
using Model.InGame.Stage;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Stage;
using View.InGame.Stage.Pawn;

namespace Installer.InGame
{
    public class StageInstaller : InstallerBase
    {
        [SerializeField] private StageMasterModel stageMasterModel;
        [SerializeField] private EventCompositeView eventCompositeView;
        [SerializeField] private StageTileView stageTileView;
        [SerializeField] private ScenePawnsView scenePawnsView;
        [SerializeField] private TileMapView tileMapView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(eventCompositeView).AsImplementedInterfaces();
            builder.RegisterInstance(stageTileView).AsImplementedInterfaces();
            builder.RegisterInstance(scenePawnsView).AsImplementedInterfaces();
            builder.RegisterInstance(tileMapView).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(stageMasterModel).AsImplementedInterfaces();
            builder.Register<TileMapModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StageFloorModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StagePawnModel>(Lifetime.Singleton).AsImplementedInterfaces();

            // Logic
            builder.Register<BurnLogic>(Lifetime.Singleton).AsImplementedInterfaces();

            // Controller
            builder.Register<StageStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<StageStateMachine>();
            builder.Register<EntryPointStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
