using UnityEditor;
using UnityEditor.SceneManagement;

namespace com.eetuvartia.UnityExtensions.Editor.SceneManagement
{

	[InitializeOnLoad]
	public class PlayModeStartSceneSelector 
	{

		private const string ShouldPlayFromStartSceneKey = "ShouldPlayFromStartScene";
		private const string ToggleMenuName              = "Edit/Play From Start Scene";

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
			get { return EditorPrefs.GetBool(ShouldPlayFromStartSceneKey, false); }
			set { EditorPrefs.SetBool(ShouldPlayFromStartSceneKey, value); }
		}

		static PlayModeStartSceneSelector()
		{
			EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(StartScenePath);
			Menu.SetChecked(ToggleMenuName, ShouldPlayFromStartScene);
		}

		[MenuItem(ToggleMenuName, priority = 140)]
		private static void ToggleShouldPlayFromStartScene()
		{
			ShouldPlayFromStartScene = !ShouldPlayFromStartScene;
			Menu.SetChecked(ToggleMenuName, ShouldPlayFromStartScene);
		}

	}

}
