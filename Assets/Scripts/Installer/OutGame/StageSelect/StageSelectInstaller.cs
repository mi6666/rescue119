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
        [SerializeField] private StageDetector3DView stageDetector3DView;
        [SerializeField] private SomeStateUiView someStateUiView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterInstance(stageDetector3DView).AsImplementedInterfaces();
            builder.RegisterInstance(someStateUiView).AsImplementedInterfaces();
            builder.RegisterInstance(someStateUiView.DifficultyLevelView).AsImplementedInterfaces();
            builder.RegisterInstance(someStateUiView.StartGameButtonView).AsImplementedInterfaces();

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