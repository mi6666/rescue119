using VContainer;
using View.InGame.Stage.Floor;
using View.InGame.Stage.Pawn;

namespace Installer.Empty
{
    public partial class EmptyImplementation
    {
        public static void RegisterEmptyView(IContainerBuilder builder)
        {
            // Stage
            builder.Register<EmptyPawnsView>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<EmptyStairEventView>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<EmptyFloorView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyPawnPoolView>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}