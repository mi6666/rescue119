using System;
using R3;
using VContainer;
using View.Global.Input;

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
        }
    }
}