using Controller.InGame.UserInterface;
using Model.InGame.Player;
using Model.InGame.Stage;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.UserInterface.Normal;
using View.InGame.UserInterface.Pause;

namespace Installer.InGame.UserInterface
{
    public class UiInstaller : InstallerBase
    {
        [SerializeField] private StageSettingModel stageSettingModel;
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
            builder.Register<HpModel>(Lifetime.Singleton).AsImplementedInterfaces();    // 仮実装
            builder.Register<TimeModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<UiStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<UiStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PauseStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}