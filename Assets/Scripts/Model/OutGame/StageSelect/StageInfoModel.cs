using Interface.ModelInterface.OutGame.StageSelect;
using Structure.OutGame;

namespace Model.OutGame.StageSelect
{
    public class StageInfoModel : IStageInfoModel
    {
        public DifficultyLevel DifficultyLevel => _difficultyLevel;

        public void SetDifficultyLevel(DifficultyLevel difficultyLevel)
        {
            _difficultyLevel = difficultyLevel;
        }

        private DifficultyLevel _difficultyLevel;
    }
}