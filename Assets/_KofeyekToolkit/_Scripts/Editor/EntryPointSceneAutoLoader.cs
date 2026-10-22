using UnityEditor;
using UnityEditor.SceneManagement;

namespace KofeyekToolkit.Editor
{
    [InitializeOnLoad]
    public static class EntryPointSceneAutoLoader
    {
        private const string MENU_PATH = "PlayFromBoot/Enable";
        private const string PLAY_FROM_BOOT_KEY = "PlayFromBootKey";
        private const int BOOT_SCENE_INDEX = 0;

        static EntryPointSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        [MenuItem(MENU_PATH)]
        private static void Toggle()
        {
            var result = EditorPrefs.GetBool(PLAY_FROM_BOOT_KEY);
            EditorPrefs.SetBool(PLAY_FROM_BOOT_KEY, !result);
        }

        [MenuItem(MENU_PATH, true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked(MENU_PATH, EditorPrefs.GetBool(PLAY_FROM_BOOT_KEY));
            return true;
        }
        
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode)
                return;

            if (!EditorPrefs.GetBool(PLAY_FROM_BOOT_KEY))
            {
                EditorSceneManager.playModeStartScene = null;
                return;
            }

            if (EditorBuildSettings.scenes.Length == 0)
                return;
            
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[BOOT_SCENE_INDEX].path);
        }
    }
}
