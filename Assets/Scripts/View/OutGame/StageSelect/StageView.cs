using Module.SceneReference.Runtime;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class StageView : MonoBehaviour
    {
        [SerializeField] private SceneGroup stageName;

        public SceneGroup StageName => stageName;
    }
}