using Interface.ModelInterface.InGame;
using Module.SceneReference.Runtime;
using Structure.Global;
using UnityEngine;

namespace Model.InGame.UserInterface
{
    [CreateAssetMenu(fileName = nameof(PrimaryMasterData), menuName = MenuName)]
    public class PrimaryMasterData: ScriptableObject, IFloorMoveTime, IExitGameSceneModel
    {
        private const string MenuName = Constants.MasterDataDiv + nameof(PrimaryMasterData);

        [SerializeField] private float floorMovePanelTime;
        [SerializeField] private SceneGroup stageSelectScene;

        public float FloorTime => floorMovePanelTime;
        public SceneGroup StageSelect => stageSelectScene;
    }
}