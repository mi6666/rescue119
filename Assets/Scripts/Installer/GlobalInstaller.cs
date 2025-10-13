using System;
using Model.OutGame.StageSelect;
using Presenter.Global;
using R3;
using VContainer;
using View.Global.Input;
using View.Global.Scene;

namespace Installer
{
    public class GlobalInstaller : InstallerBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);
            builder.Register(_ => new CompositeDisposable(), Lifetime.Scoped)
                .As<CompositeDisposable, IDisposable>();
            
            // View
            builder.Register<InputWrapper>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoaderView>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Model
            builder.Register<StageInfoModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Presenter
            builder.Register<ScenePresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}