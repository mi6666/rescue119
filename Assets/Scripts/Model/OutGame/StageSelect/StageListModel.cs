
using System.Collections.Generic;
using Interface.ModelInterface.OutGame.StageSelect;

namespace Model.OutGame.StageSelect
{
    public class StageListModel : IStageListModel
    {
        // 本来はScriptableObjectなどから取得する
        public IReadOnlyList<string> StageNames => new[]
        {
            "Stage 1",
            "Stage 2",
            "Stage 3"
        };
    }
}
