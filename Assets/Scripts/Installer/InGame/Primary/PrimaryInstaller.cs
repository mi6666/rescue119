using Controller.InGame.Primary;
using Model.InGame.Player;
using Model.InGame.Stage;
using Model.InGame.UserInterface;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.UserInterface.Normal;
using View.InGame.UserInterface.Pause;

namespace Installer.InGame.Primary
{
    public class PrimaryInstaller : InstallerBase
    {
        [SerializeField] private StageSettingModel stageSettingModel;
        [SerializeField] private ExitGameSceneModel exitGameSceneModel;
        [SerializeField] private NormalUiView normalUiView;
        [SerializeField] private PauseUiView pauseUiView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(normalUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.TimerView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.HpUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.PauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiView.ExitPauseButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(pauseUiView.ExitStageButtonView).AsImplementedInterfaces();
            
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
        }
    }
}