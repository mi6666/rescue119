using UnityEngine;

namespace Module.EditorExtension.Runtime
{
    [CreateAssetMenu(fileName = "SceneEnumGeneratorSettings", menuName = "Editor Extensions/Scene Enum Generator Settings")]
    public class SceneEnumGeneratorSettings : ScriptableObject
    {
        [Header("Input")]
        [Tooltip("The directory to search for scene files.")]
        public string searchDirectory = "Assets/Scenes";

        [Header("Output")]
        [Tooltip("The directory to output the generated enum file.")]
        public string outputDirectory = "Assets/Scripts/Structure/Global";
        
        [Tooltip("The name of the enum to be generated.")]
        public string enumName = "SceneName";

        [Tooltip("The file name of the generated C# script.")]
        public string fileName = "SceneName.cs";
    }
}
