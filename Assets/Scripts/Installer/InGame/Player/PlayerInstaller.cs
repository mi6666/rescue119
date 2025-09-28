using Controller.InGame.Player;
using Logic.InGame.Player;
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
        [SerializeField] private LocomotionModel locomotionModel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("configure");
            // View
            builder.RegisterInstance(playerView).AsImplementedInterfaces();
            
            // Model
            builder.RegisterInstance(locomotionModel).AsImplementedInterfaces();
            
            // Logic
            builder.Register<LocomotionLogic>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<PlayerState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<PlayerStateMachine>();
            builder.Register<IdleStateBehaviour>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}