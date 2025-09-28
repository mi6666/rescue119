
using System.Collections.Generic;

namespace Interface.ModelInterface.OutGame.StageSelect
{
    public interface IStageListModel
    {
        IReadOnlyList<string> StageNames { get; }
    }
}
