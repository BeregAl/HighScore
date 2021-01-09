using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnityToolbarExtender.Examples
{
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;

        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 15,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold
            };
        }
    }

    [InitializeOnLoad]
    public class SceneSwitchLeftButton
    {
        static SceneSwitchLeftButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();
            var tex = EditorGUIUtility.IconContent(@"ClothInspector.PaintValue").image;

            if (GUILayout.Button(new GUIContent("M", "Open Menu scene"), ToolbarStyles.commandButtonStyle))
            {
                SceneHelper.OpenSceneAdditionally("Menu");
            }

            if (GUILayout.Button(new GUIContent("G", "Open Game scene"), ToolbarStyles.commandButtonStyle))
            {
                SceneHelper.OpenSceneAdditionally("Game");
            }
        }
    }

    static class SceneHelper
    {
        public static void OpenSceneSingle(string sceneName)
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            OpenSceneByName(sceneName);
        }
        
        public static void OpenSceneAdditionally(string sceneName)
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }

            OpenSceneByName("StartScene");
            OpenSceneByName(sceneName, OpenSceneMode.Additive);
        }

        private static void OpenSceneByName(string sceneName, OpenSceneMode mode = OpenSceneMode.Single)
        {
            string[] guids = AssetDatabase.FindAssets("t:scene " + sceneName, null);
            if (guids.Length == 0)
            {
                Debug.LogWarning("Couldn't find scene file");
            }
            else
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                EditorSceneManager.OpenScene(scenePath, mode);
            }
        }
    }
}