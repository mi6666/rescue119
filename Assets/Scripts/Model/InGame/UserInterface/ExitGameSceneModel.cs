using System;
using Interface.ModelInterface.InGame;
using Module.SceneReference.Runtime;
using UnityEngine;

namespace Model.InGame.UserInterface
{
    [Serializable]
    public class ExitGameSceneModel:IExitGameSceneModel
    {
        [SerializeField] private SceneGroup stageSelectScene;
        public SceneGroup StageSelect => stageSelectScene;
    }
}