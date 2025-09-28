using Cysharp.Threading.Tasks;
using Module.SceneReference.Runtime;
using UnityEngine;

namespace Interface.ViewInterface.Global
{
    /// <summary>
    /// シーン読み込みを行うView
    /// </summary>
    public interface ISceneLoaderView
    {
        public UniTask<AsyncOperation> LoadScene(string scenePath);
        public void ActivateAsync(AsyncOperation sceneOperation);
        public void SetActiveScene(string scenePath);
        public UniTask UnLoadScene(string scenePath);

        public UnityEngine.SceneManagement.Scene CurrentScene { get; }
    }
}