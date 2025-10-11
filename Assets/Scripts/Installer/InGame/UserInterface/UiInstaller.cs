using Controller.InGame.UserInterface;
using Model.InGame.Player;
using Model.InGame.Stage;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.UserInterface.Normal;

namespace Installer.InGame.UserInterface
{
    public class UiInstaller : InstallerBase
    {
        [SerializeField] private NormalUiView normalUiView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(normalUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.TimerView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.HpUiView).AsImplementedInterfaces();
            builder.RegisterInstance(normalUiView.PauseButtonView).AsImplementedInterfaces();
            
            // Model
            builder.Register<HpModel>(Lifetime.Singleton).AsImplementedInterfaces();    // 仮実装
            builder.Register<TimeModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<UiStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<UiStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}