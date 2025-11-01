using System;
using Controller.InGame.Common;
using Installer.Empty;
using Logic.InGame.Stage;
using Model.InGame.Stage;
using Model.OutGame.StageSelect;
using Presenter.Global;
using Presenter.InGame.Stage;
using R3;
using VContainer;
using View.Global.Input;
using View.Global.Scene;
using View.InGame;

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
            builder.Register<PrimaryStateEventView>(Lifetime.Singleton).AsImplementedInterfaces();
            EmptyImplementation.RegisterEmptyView(builder);
            
            // Model
            builder.Register<StageInfoModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StageFloorModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StagePawnModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FloorMoveContextModel>(Lifetime.Singleton).AsImplementedInterfaces();
            EmptyImplementation.RegisterEmptyModel(builder);
            
            // Presenter
            builder.Register<ScenePresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyMapPresenter>(Lifetime.Transient).AsImplementedInterfaces();
            
            // Logic
            builder.Register<GridCastLogic>(Lifetime.Transient).AsImplementedInterfaces();
            
            // Connection
            builder.Register<PawnConnection>(Lifetime.Transient);
            builder.Register<LocomotionConnection>(Lifetime.Transient);
        }
    }
}