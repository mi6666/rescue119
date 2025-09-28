using UnityEngine.UI;

namespace Interface.ViewInterface.OutGame.StageSelect
{
    public interface IStageSelectItemView
    {
        Button SelectButton { get; }
        void SetStageName(string stageName);
        string StageName { get; }
    }
}