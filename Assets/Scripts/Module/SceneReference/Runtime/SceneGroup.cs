using UnityEngine;

namespace Module.SceneReference.Runtime
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "SceneGroup")]
    public class SceneGroup : ScriptableObject
    {
        [SerializeField] private SceneField primaryScene;
        [SerializeField] private SceneField[] subScenes;

        private string[] _subScenePathsCache;
        private bool _isCacheDirty = true;

        public string PrimaryScene => primaryScene;
        public string[] SubScenes
        {
            get
            {
                if (_isCacheDirty || _subScenePathsCache == null)
                {
                    if (subScenes == null)
                    {
                        _subScenePathsCache = System.Array.Empty<string>();
                    }
                    else
                    {
                        _subScenePathsCache = new string[subScenes.Length];
                        for (var i = 0; i < subScenes.Length; i++)
                        {
                            _subScenePathsCache[i] = subScenes[i];
                        }
                    }
                    _isCacheDirty = false;
                }
                return _subScenePathsCache;
            }
        }

        private void OnValidate()
        {
            _isCacheDirty = true;
        }
    }
}
