using Module.SceneReference.Runtime;

namespace Interface.ModelInterface.InGame
{
    /// <summary>
    /// ゲームから出る際に向かうシーン情報を持つ
    /// </summary>
    public interface IExitGameSceneModel
    {
        public SceneGroup StageSelect { get; }
    }
}