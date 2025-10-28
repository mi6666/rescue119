using Controller.InGame.Player;
using Logic.InGame.Player;
using Logic.InGame.Stage;
using Model.InGame.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Player;

namespace Installer.InGame.Player
{
    public class PlayerInstaller: InstallerBase
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerAnimatorView playerAnimatorView;
        [SerializeField] private DetectPositionView detectPositionView;
        [SerializeField] private WaterView waterView;
        [SerializeField] private LocomotionModel locomotionModel;
        [SerializeField] private ActionModel actionModel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(playerView).AsImplementedInterfaces();
            builder.RegisterInstance(playerAnimatorView).AsImplementedInterfaces();
            builder.RegisterInstance(detectPositionView).AsImplementedInterfaces();
            builder.RegisterInstance(waterView).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(locomotionModel).AsImplementedInterfaces();
            builder.RegisterInstance(actionModel).AsImplementedInterfaces();
            builder.Register<CurrentLookModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Logic
            builder.Register<LocomotionLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<PlayerStateEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ActionStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}