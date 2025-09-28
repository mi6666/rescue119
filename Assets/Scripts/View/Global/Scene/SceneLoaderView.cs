using Cysharp.Threading.Tasks;
using Interface.ViewInterface.Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class SceneLoaderView : ISceneLoaderView
    {
        public async UniTask<AsyncOperation> LoadScene(string scenePath)
        {
            var operation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            operation!.allowSceneActivation = false;
            await operation.ToUniTask();
            return operation;
        }

        public void ActivateAsync(AsyncOperation sceneOperation)
        {
            sceneOperation.allowSceneActivation = true;
        }

        public void SetActiveScene(string scenePath)
        {
            var loadedScene = SceneManager.GetSceneByPath(scenePath);
            if (loadedScene.IsValid() && loadedScene.isLoaded)
            {
                SceneManager.SetActiveScene(loadedScene);
            }
            else
            {
                Debug.LogWarning($"シーン {scenePath} は有効でないかロードされていません。");
            }
        }

        public async UniTask UnLoadScene(string scenePath)
        {
            await SceneManager.UnloadSceneAsync(scenePath);
        }

        public UnityEngine.SceneManagement.Scene CurrentScene => SceneManager.GetActiveScene();
    }
}