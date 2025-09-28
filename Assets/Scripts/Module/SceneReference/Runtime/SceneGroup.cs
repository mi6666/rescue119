using System;
using UnityEngine;

namespace Module.SceneReference.Runtime
{
    [Serializable]
    public class SceneGroup
    {
        [SerializeField] private string primaryScene;
        [SerializeField] private string[] subScenes;

        public string PrimaryScene => primaryScene;
        public string[] SubScenes => subScenes;
    }
}