using System;
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
using View.InGame.Stage.Pawn;

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
            builder.Register<EmptyPawnsView>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<EmptyStairEventView>(Lifetime.Transient).AsImplementedInterfaces();
            
            // Model
            builder.Register<StageInfoModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StageFloorModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StagePawnModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FloorMoveContextModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyTileMapModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Presenter
            builder.Register<ScenePresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EmptyMapPresenter>(Lifetime.Transient).AsImplementedInterfaces();
            
            // Logic
            builder.Register<GridCastLogic>(Lifetime.Transient).AsImplementedInterfaces();
        }
    }
}