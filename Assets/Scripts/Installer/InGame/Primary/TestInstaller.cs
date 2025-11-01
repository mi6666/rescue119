using Controller.InGame.Primary;
using Model.InGame.Player;
using Model.InGame.Stage;
using Model.InGame.UserInterface;
using Structure.InGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Stage;
using View.InGame.UserInterface.FloorMove;
using View.InGame.UserInterface.GameClear;
using View.InGame.UserInterface.GameOver;
using View.InGame.UserInterface.Normal;
using View.InGame.UserInterface.Pause;

namespace Installer.InGame.Primary
{
    public class TestInstaller : LifetimeScope
    {
        [SerializeField] private EventCompositeView eventCompositeView;
        [SerializeField] private StageMasterModel stageMasterModel;
        [SerializeField] private ExitGameSceneModel exitGameSceneModel;
        [SerializeField] private NormalUiFadeView normalUiFadeView;
        [SerializeField] private PauseUiFadeView pauseUiFadeView;
        [SerializeField] private GameOverUiFadeView gameOverUiFadeView;
        [SerializeField] private GameClearUiFadeView gameClearUiFadeView;
        [SerializeField] private FloorMoveUiFadeView floorMoveUiFadeView;
        [SerializeField] private PlayerMasterData playerMasterData;
        [SerializeField] private PrimaryMasterData primaryMasterData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(eventCompositeView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiFadeView.TimerView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiFadeView.HpUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiFadeView.PauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiFadeView.ExitPauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiFadeView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.Pause);
            builder.RegisterInstance(gameClearUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(gameClearUiFadeView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.GameClear);
            builder.RegisterInstance(gameOverUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(gameOverUiFadeView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.GameOver);
            builder.RegisterInstance(floorMoveUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(floorMoveUiFadeView.FloorMoveTextView).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(stageMasterModel).AsImplementedInterfaces();
            builder.RegisterInstance(exitGameSceneModel).AsImplementedInterfaces();
            builder.RegisterInstance(playerMasterData).AsImplementedInterfaces();
            builder.RegisterInstance(primaryMasterData).AsImplementedInterfaces();
            builder.Register<HpModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<PrimaryStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<PrimaryStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PauseStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameClearStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameOverStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FloorTransitionStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}