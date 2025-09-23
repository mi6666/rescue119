using UnityEngine;

namespace Module.SceneReference.Runtime
{
    public enum SceneType
    {
        SceneManager,
        Addressable,
    }

    public readonly struct SceneContext
    {
        public static SceneContext SceneManagerContext(AsyncOperation operation, string scenePath)
        {
            return new SceneContext(SceneType.SceneManager, operation, scenePath);
        }

        private SceneContext
        (
            SceneType sceneType,
            AsyncOperation operation,
            string scenePath
        )
        {
            Type = sceneType;
            Operation = operation;
            ScenePath = scenePath;
        }

        public SceneType Type { get; }
        public AsyncOperation Operation { get; }
        public string ScenePath { get; }
    }
}