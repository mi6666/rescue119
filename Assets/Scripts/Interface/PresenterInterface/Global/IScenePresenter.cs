using Cysharp.Threading.Tasks;
using Module.SceneReference.Runtime;
using R3;
using Structure.Global;

namespace Interface.PresenterInterface.Global
{
    public interface IScenePresenter
    {
        public UniTask LoadScene(SceneGroup sceneGroup);
    }

    public interface ISceneEventPresenter
    {
        public Observable<SceneEventType> SceneEventObservable { get; }
    }
}