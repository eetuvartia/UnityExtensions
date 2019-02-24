using UnityEditor;
using UnityEditor.SceneManagement;

namespace com.eetuvartia.UnityExtensions.Editor.SceneManagement
{

	[InitializeOnLoad]
	internal class PlayModeStartSceneSelector
	{

		private static string StartScenePath
		{
			get
			{
				return EditorBuildSettings.scenes.Length > 0
					? EditorBuildSettings.scenes[0].path
					: null;
			}
		}

		private static bool ShouldPlayFromStartScene 
		{
			get { return EditorPrefs.GetBool(PlayModeStartSceneSelectionConstants.ShouldPlayFromStartSceneKey, false); }
			set { EditorPrefs.SetBool(PlayModeStartSceneSelectionConstants.ShouldPlayFromStartSceneKey, value); }
		}

		static PlayModeStartSceneSelector()
		{
			EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(StartScenePath);
			Menu.SetChecked
			(
				PlayModeStartSceneSelectionConstants.ToggleShouldPlayFromStartSceneMenuItem,
				ShouldPlayFromStartScene
			);
		}

		[MenuItem(PlayModeStartSceneSelectionConstants.ToggleShouldPlayFromStartSceneMenuItem, priority = 140)]
		private static void ToggleShouldPlayFromStartScene()
		{
			ShouldPlayFromStartScene = !ShouldPlayFromStartScene;
			Menu.SetChecked
			(
				PlayModeStartSceneSelectionConstants.ToggleShouldPlayFromStartSceneMenuItem,
				ShouldPlayFromStartScene
			);
		}

	}

}
