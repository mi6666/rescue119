using System.Linq;
using Module.SceneReference.Runtime;
using UnityEditor;
using UnityEngine;

namespace Module.SceneReference.Editor
{
    /// <summary>
    /// Provides a <see cref="PropertyDrawer"/> for <see cref="SceneField"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        private GUIStyle _pathLabelStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var sceneAssetProp = property.FindPropertyRelative("sceneAsset");
            var scenePathProp = property.FindPropertyRelative("scenePath");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUI.ObjectField(
                position,
                sceneAssetProp.objectReferenceValue,
                typeof(SceneAsset),
                false
            );

            if (EditorGUI.EndChangeCheck())
            {
                sceneAssetProp.objectReferenceValue = newScene;
                scenePathProp.stringValue = newScene ? AssetDatabase.GetAssetPath(newScene) : "";
            }

            // Lazy initialize GUIStyle once
            if (_pathLabelStyle == null)
            {
                _pathLabelStyle = new GUIStyle(EditorStyles.label);
            }

            // 色を判定
            if (!string.IsNullOrEmpty(scenePathProp.stringValue)
                && !EditorBuildSettings.scenes.Any(s => s.path == scenePathProp.stringValue))
            {
                _pathLabelStyle.normal.textColor = Color.red;
            }
            else
            {
                _pathLabelStyle.normal.textColor = EditorStyles.label.normal.textColor;
            }

            // パス表示
            if (!string.IsNullOrEmpty(scenePathProp.stringValue))
            {
                var labelPos = position;
                labelPos.x += position.width + 5;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.LabelField(labelPos, scenePathProp.stringValue, _pathLabelStyle);
                EditorGUI.EndDisabledGroup();
            }

            EditorGUI.EndProperty();
        }
    }
}