using Controller.InGame.Player;
using Logic.InGame.Player;
using Model.InGame.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.InGame.Player;

namespace Installer.InGame.Player
{
    public class LastPlayerInstaller: InstallerBase
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private HoldingPawnView holdingPawnView;
        [SerializeField] private PlayerAnimatorView playerAnimatorView;
        [SerializeField] private DetectPositionView detectPositionView;
        [SerializeField] private WaterView waterView;
        [SerializeField] private PlayerMasterData playerMasterData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(playerView).AsImplementedInterfaces();
            builder.RegisterInstance(holdingPawnView).AsImplementedInterfaces();
            builder.RegisterInstance(playerAnimatorView).AsImplementedInterfaces();
            builder.RegisterInstance(detectPositionView).AsImplementedInterfaces();
            builder.RegisterInstance(waterView).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(playerMasterData).AsImplementedInterfaces();
            builder.Register<CurrentLookModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerLockModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LocomotionModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Logic
            builder.Register<LocomotionLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<PlayerStateEntity>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<NormalStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ActionStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StopStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<HoldingStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}