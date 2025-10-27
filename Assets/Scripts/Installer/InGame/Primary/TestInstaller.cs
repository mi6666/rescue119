using Controller.InGame.Primary;
using Model.InGame.Player;
using Model.InGame.Stage;
using Model.InGame.UserInterface;
using Structure.InGame;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;
using View.InGame.UserInterface.FloorMove;
using View.InGame.UserInterface.GameClear;
using View.InGame.UserInterface.GameOver;
using View.InGame.UserInterface.Normal;
using View.InGame.UserInterface.Pause;

namespace Installer.InGame.Primary
{
    public class TestInstaller : LifetimeScope
    {
        [SerializeField] private ClearButtonView clearButtonView;
        [SerializeField] private GameOverButtonView gameOverButtonView;
        [SerializeField] private StageSettingModel stageSettingModel;
        [SerializeField] private ExitGameSceneModel exitGameSceneModel;
        [SerializeField] private NormalUiFadeView normalUiFadeView;
        [SerializeField] private PauseUiFadeView pauseUiFadeView;
        [SerializeField] private GameOverUiFadeView gameOverUiFadeView;
        [SerializeField] private GameClearUiFadeView gameClearUiFadeView;
        [FormerlySerializedAs("floorMoveUiView")] [SerializeField] private FloorMoveUiFadeView floorMoveUiFadeView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(normalUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiFadeView.TimerView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiFadeView.HpUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiFadeView.PauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiFadeView.ExitPauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiFadeView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.Pause);
            builder.RegisterInstance(gameClearUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(gameClearUiFadeView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.GameClear);
            builder.RegisterInstance(clearButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(gameOverUiFadeView).AsImplementedInterfaces();
            builder.RegisterInstance(gameOverUiFadeView.ExitStageButtonView).AsImplementedInterfaces().Keyed(PrimaryStateType.GameOver);
            builder.RegisterInstance(gameOverButtonView).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(stageSettingModel).AsImplementedInterfaces();
            builder.RegisterInstance(exitGameSceneModel).AsImplementedInterfaces();
            builder.Register<HpModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<PrimaryStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<PrimaryStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PauseStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameClearStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameOverStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}