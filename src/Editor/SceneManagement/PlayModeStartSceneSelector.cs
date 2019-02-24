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
				return ShouldPlayFromStartScene
					? EditorPrefs.GetString(PlayModeStartSceneSelectionConstants.StartUpSceneAssetPathKey, null)
					: null;
			}
		}

		private static bool ShouldPlayFromStartScene
		{
			get { return EditorPrefs.GetBool(PlayModeStartSceneSelectionConstants.ShouldPlayFromStartSceneKey, false); }
			set
			{
				EditorSceneManager.playModeStartScene = value ? StartUpSceneAsset : null;
				EditorPrefs.SetBool(PlayModeStartSceneSelectionConstants.ShouldPlayFromStartSceneKey, value);
			}
		}

		private static SceneAsset StartUpSceneAsset
		{
			get { return AssetDatabase.LoadAssetAtPath<SceneAsset>(StartScenePath); }
		}

		static PlayModeStartSceneSelector()
		{
			EditorSceneManager.playModeStartScene = StartUpSceneAsset;
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
