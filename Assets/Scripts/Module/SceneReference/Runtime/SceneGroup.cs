using System.Collections.Generic;
using UnityEngine;

namespace Module.SceneReference.Runtime
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "SceneGroup")]
    public class SceneGroup : ScriptableObject
    {
        [SerializeField] private SceneEnum primaryScene;
        [SerializeField] private List<SceneEnum> subScenes;

        private string _primarySceneName;
        private string[] _subSceneNames;

        public string PrimaryScene => _primarySceneName;
        public IReadOnlyList<string> SubScenes => _subSceneNames;

        private void OnEnable()
        {
            _primarySceneName = primaryScene.ToString();

            if (subScenes == null)
            {
                _subSceneNames = System.Array.Empty<string>();
                return;
            }

            if (_subSceneNames == null || _subSceneNames.Length != subScenes.Count)
            {
                _subSceneNames = new string[subScenes.Count];
            }

            for (var i = 0; i < subScenes.Count; i++)
            {
                _subSceneNames[i] = subScenes[i].ToString();
            }
        }
    }
}