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
    public class PrimaryInstaller : InstallerBase
    {
        [SerializeField] private EventCompositeView eventCompositeView;
        [SerializeField] private ClearButtonView clearButtonView;
        [SerializeField] private GameOverButtonView gameOverButtonView;
        [SerializeField] private StageMasterModel stageMasterModel;
        [SerializeField] private PrimaryMasterData primaryMasterData;
        [SerializeField] private NormalUiView normalUiView;
        [SerializeField] private PauseUiView pauseUiView;
        [SerializeField] private GameOverUiView gameOverUiView;
        [SerializeField] private GameClearUiView gameClearUiView;
        [SerializeField] private FloorMoveUiView floorMoveUiView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(eventCompositeView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.TimerView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.HpUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.PauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiView.ExitPauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.Pause);
            builder.RegisterInstance(gameClearUiView).AsImplementedInterfaces();
            builder.RegisterInstance(gameClearUiView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.GameClear);
            builder.RegisterInstance(clearButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(gameOverUiView).AsImplementedInterfaces();
            builder.RegisterInstance(gameOverUiView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.GameOver);
            builder.RegisterInstance(gameOverButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(floorMoveUiView).AsImplementedInterfaces();
            builder.RegisterInstance(floorMoveUiView.FloorMoveTextView).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(stageMasterModel).AsImplementedInterfaces();
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