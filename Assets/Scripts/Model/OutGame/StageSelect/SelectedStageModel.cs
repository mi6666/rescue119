using Interface.ModelInterface.OutGame.StageSelect;

namespace Model.OutGame.StageSelect
{

    public class SelectedStageModel:ISelectedStageModel
    {
        public void SetSelectedStage(string stage)
        {
            _selectedStage = stage;
        }

        public string GetSelectedStage()
        {
            return _selectedStage;
        }

        private string _selectedStage;
    }    
}
