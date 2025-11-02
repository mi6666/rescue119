using Model.InGame.Player;
using Model.InGame.Stage;
using VContainer;

namespace Installer.Empty
{
    public partial class EmptyImplementation
    {
        public static void RegisterEmptyModel(IContainerBuilder builder)
        {
            // Stage
            builder.Register<EmptyTileMapModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyStageMasterModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Player
            builder.Register<EmptyHpModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyLocomotionSetting>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyAnimationKeyModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyActionSetting>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}