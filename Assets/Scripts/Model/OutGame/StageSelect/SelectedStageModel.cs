using Interface.ModelInterface.OutGame.StageSelect;
using Module.SceneReference.Runtime;

namespace Model.OutGame.StageSelect
{
    public class SelectedStageModel : ISelectedStageModel
    {
        public void SetSelectedStage(SceneGroup stage)
        {
            _selectedStage = stage;
        }

        public SceneGroup GetSelectedStage()
        {
            return _selectedStage;
        }

        private SceneGroup _selectedStage;
    }
}