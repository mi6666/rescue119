using Module.SceneReference.Runtime;

namespace Interface.ModelInterface.OutGame.StageSelect
{
    public interface ISelectedStageModel
    {
        public void SetSelectedStage(SceneGroup stage);
        public SceneGroup GetSelectedStage();
    }
}