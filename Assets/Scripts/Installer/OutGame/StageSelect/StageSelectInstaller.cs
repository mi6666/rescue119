using Controller.OutGame.StageSelect;
using Model.OutGame.StageSelect;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.OutGame.StageSelect;

namespace Installer.OutGame.StageSelect
{
    public class StageSelectInstaller : LifetimeScope
    {
        [SerializeField] private StageDetectorView stageDetectorView;
        [SerializeField] private DifficultyLevelView difficultyLevelView;
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(stageDetectorView).AsImplementedInterfaces();
            builder.RegisterInstance(difficultyLevelView).AsImplementedInterfaces();

            // Model
            builder.Register<SelectedStageModel>(Lifetime.Scoped).AsImplementedInterfaces();

            // Presenter
            
            // Controller
            builder.Register<StageSelectStateEntity>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<StageSelectStateMachine>();
            builder.Register<NoneStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SomeStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}