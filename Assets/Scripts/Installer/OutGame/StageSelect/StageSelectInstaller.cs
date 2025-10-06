using Interface.ModelInterface.OutGame.StageSelect;
using Presenter.OutGame.StageSelect;
using VContainer;
using VContainer.Unity;

namespace Installer.OutGame.StageSelect
{
    public class StageSelectInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // View

            // Model

            // Presenter
            builder.RegisterEntryPoint<StageSelectPresenter>();
        }
    }
}