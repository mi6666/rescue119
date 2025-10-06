using UnityEngine;

namespace Module.SceneReference.Runtime
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "SceneGroup")]
    public class SceneGroup : ScriptableObject
    {
        [SerializeField] private SceneField primaryScene;
        [SerializeField] private SceneField[] subScenes;

        public string PrimaryScene => primaryScene;
        public string[] SubScenes { get; private set; }

        private void OnEnable()
        {
            SubScenes = new string[subScenes.Length];

            for (int i = 0; i < subScenes.Length; i++)
            {
                SubScenes[i] = subScenes[i];
            }
        }
    }
}