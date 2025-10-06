using System.Collections.Generic;
using UnityEngine;

namespace Module.SceneReference.Runtime
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "SceneGroup")]
    public class SceneGroup : ScriptableObject
    {
        [SerializeField] private SceneField primaryScene;
        [SerializeField] private List<SceneField> subScenes;

        public string PrimaryScene => primaryScene;
        public string[] SubScenes { get; private set; }
    }
}