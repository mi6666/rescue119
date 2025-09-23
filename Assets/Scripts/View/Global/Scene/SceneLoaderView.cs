using Cysharp.Threading.Tasks;
using Interface.View.Global;
using Module.SceneReference.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class SceneLoaderView : ISceneLoaderView
    {
        public async UniTask<SceneContext> LoadScene(string scenePath)
        {
            // アセットが存在しない
            var operation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            operation!.allowSceneActivation = false;
            await operation.ToUniTask();
            return SceneContext.SceneManagerContext(operation, scenePath);
        }

        public UniTask ActivateAsync(SceneContext scene)
        {
            scene.Operation.allowSceneActivation = true;
            return UniTask.CompletedTask;
        }

        public void SetActiveScene(SceneContext scene)
        {
            var loadedScene = SceneManager.GetSceneByPath(scene.ScenePath);
            if (loadedScene.IsValid() && loadedScene.isLoaded)
            {
                SceneManager.SetActiveScene(loadedScene);
            }
            else
            {
                Debug.LogWarning($"シーン {scene.ScenePath} は有効でないかロードされていません。");
            }
        }

        public async UniTask UnLoadScene(SceneContext sceneInstance)
        {
            await SceneManager.UnloadSceneAsync(sceneInstance.ScenePath);
        }

        public UnityEngine.SceneManagement.Scene CurrentScene => SceneManager.GetActiveScene();
    }
}