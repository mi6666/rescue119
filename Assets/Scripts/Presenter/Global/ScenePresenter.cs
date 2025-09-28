using Cysharp.Threading.Tasks;
using Interface.PresenterInterface.Global;
using Interface.ViewInterface.Global;
using Module.Option.Runtime;
using Module.SceneReference.Runtime;
using R3;
using Structure.Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Presenter.Global
{
    public class ScenePresenter : IScenePresenter, ISceneEventPresenter
    {
        public ScenePresenter
        (
            ISceneLoaderView sceneLoaderView,
            CompositeDisposable compositeDisposable
        )
        {
            SceneLoaderView = sceneLoaderView;
            SceneEventSubject.AddTo(compositeDisposable);
        }

        public async UniTask LoadScene(SceneGroup sceneGroup)
        {
            await InvokeSceneEvent(SceneEventType.StartLoadScene);

            await InvokeSceneEvent(SceneEventType.BeforeSceneLoad);
            await InnerLoadScene(sceneGroup);
            await InvokeSceneEvent(SceneEventType.AfterSceneLoad);

            await InvokeSceneEvent(SceneEventType.BeforeSceneUnLoad);
            await UnLoadScene();
            await InvokeSceneEvent(SceneEventType.AfterSceneUnLoad);

            _currentSceneGroup = sceneGroup;
            await InvokeSceneEvent(SceneEventType.EndLoadScene);
        }

        private async UniTask InnerLoadScene(SceneGroup group)
        {
            var primarySceneOperation = await SceneLoaderView.LoadScene(group.PrimaryScene);
            SceneLoaderView.ActivateAsync(primarySceneOperation);

            var primaryScene = SceneManager.GetSceneByPath(group.PrimaryScene);
            var primarySceneRootObject = primaryScene.GetRootGameObjects();
            LifetimeScope rootScope = null;

            for (int i = 0; i < primarySceneRootObject.Length; i++)
            {
                var lifetimeScopes = primarySceneRootObject[i].GetComponentsInChildren<LifetimeScope>()!;

                foreach (var scope in lifetimeScopes)
                {
                    // primaryシーンには`LifetimeScope`が一つだけ存在すること
                    Debug.Assert(rootScope is null);

                    rootScope = scope;
                    Debug.Log($"scene: {primaryScene.name}, lifetime scope: {scope.name}");
                    await UniTask.RunOnThreadPool(lifetimeScope => (lifetimeScope as LifetimeScope)!.Build(), scope);
                }
            }

            using var _ = LifetimeScope.EnqueueParent(rootScope);

            for (int i = 0; i < group.SubScenes.Length; i++)
            {
                var subScenePath = group.SubScenes[i];

                var subSceneOperation = await SceneLoaderView.LoadScene(subScenePath);
                SceneLoaderView.ActivateAsync(subSceneOperation);

                var subScene = SceneManager.GetSceneByPath(subScenePath);
                var rootObjects = subScene.GetRootGameObjects();

                foreach (var rootObject in rootObjects)
                {
                    var lifetimeScopes = rootObject.GetComponentsInChildren<LifetimeScope>()!;

                    foreach (var scope in lifetimeScopes)
                    {
                        await UniTask.RunOnThreadPool(lifetimeScope => (lifetimeScope as LifetimeScope)!.Build(),
                            scope);
                    }
                }
            }
        }

        private async UniTask UnLoadScene()
        {
            foreach (var subScene in _currentSceneGroup.SubScenes)
            {
                await SceneLoaderView.UnLoadScene(subScene);
            }

            await SceneLoaderView.UnLoadScene(_currentSceneGroup.PrimaryScene);
        }

        private async UniTask InvokeSceneEvent(SceneEventType eventType)
        {
            SceneEventSubject.OnNext(eventType);
            await UniTask.WaitWhile(SceneBlock, block => block.IsAnyBlocked());
        }

        public Observable<SceneEventType> SceneEventObservable => SceneEventSubject;

        private SceneGroup _currentSceneGroup;
        private OperationPool SceneBlock { get; } = new OperationPool();
        private Subject<SceneEventType> SceneEventSubject { get; } = new Subject<SceneEventType>();
        private ISceneLoaderView SceneLoaderView { get; }
    }
}