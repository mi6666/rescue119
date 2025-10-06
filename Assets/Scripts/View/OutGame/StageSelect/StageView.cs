using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class StageView : MonoBehaviour
    {
        [SerializeField] private string stageName;

        public string StageName => stageName;
    }
}