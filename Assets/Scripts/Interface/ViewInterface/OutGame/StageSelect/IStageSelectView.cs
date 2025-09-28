
using R3;
using UnityEngine;

namespace Interface.ViewInterface.OutGame.StageSelect
{
    public interface IStageSelectView
    {
        Transform ItemParent { get; }
        Observable<string> OnStageSelected { get; }
        void AddSelectItem(IStageSelectItemView itemView);
    }
}
