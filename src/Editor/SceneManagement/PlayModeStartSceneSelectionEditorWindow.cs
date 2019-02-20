using UnityEditor;
using UnityEngine;

namespace com.eetuvartia.UnityExtensions.Editor.SceneManagement
{

	internal class PlayModeStartSceneSelectionEditorWindow : EditorWindow
	{

		private SceneAsset startUpSceneAsset;
		private SceneAsset StartUpSceneAsset
		{
			get { return startUpSceneAsset; }
			set
			{
				startUpSceneAsset = value;
				StartScenePath    = AssetDatabase.GetAssetPath(value);
			}
		}

		private static string StartScenePath { 
			get { return EditorPrefs.GetString(PlayModeStartSceneSelectionConstants.StartUpSceneAssetPathKey, null); }
			set { EditorPrefs.SetString(PlayModeStartSceneSelectionConstants.StartUpSceneAssetPathKey, value);}
		}

		[MenuItem(PlayModeStartSceneSelectionConstants.OpenPlayModeStartSceneSelectionEditorWindowMenuItem)]
		private static void Open()
		{
			#region Workaround to get inspector type

			var editorAsm       = typeof(UnityEditor.Editor).Assembly;
			var typeOfInspector = editorAsm.GetType("UnityEditor.InspectorWindow");

			#endregion

			var window = GetWindow<PlayModeStartSceneSelectionEditorWindow>("Editor Start Up Scene Editor", typeOfInspector);
			window.Initialize();
			window.Show();
		}

		private void Initialize()
		{
			StartUpSceneAsset = GetSceneAssetWithPath(StartScenePath);
		}

		private static SceneAsset GetSceneAssetWithPath(string path)
		{
			return AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
		}

		private void OnGUI()
		{
			StartUpSceneAsset = EditorGUILayout.ObjectField
			(
				"Play Mode Start Up Scene",
				StartUpSceneAsset, 
				typeof(SceneAsset), 
				false
			) as SceneAsset;

			EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Clear Scene Selection"))
			{
				StartUpSceneAsset = null;
			}

			if (EditorBuildSettings.scenes.Length > 0 && GUILayout.Button("Set As First From Build Settings"))
			{
				var path = EditorBuildSettings.scenes[0].path;
				StartUpSceneAsset = GetSceneAssetWithPath(path);
			}

			EditorGUILayout.EndHorizontal();
		}

	}

}
