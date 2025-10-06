
using System.Collections.Generic;
using Structure.OutGame;

namespace Interface.ModelInterface.OutGame.StageSelect
{
    public interface IStageInfoModel
    {
        public DifficultyLevel DifficultyLevel { get; }
        public void SetDifficultyLevel(DifficultyLevel difficultyLevel);
    }
}
