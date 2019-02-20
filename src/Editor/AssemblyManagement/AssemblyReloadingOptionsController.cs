using UnityEditor;
using UnityEngine;

namespace com.eetuvartia.UnityExtensions.Editor.AssemblyManagement
{

	[InitializeOnLoad]
	public class AssemblyReloadingOptionsController
	{

		static AssemblyReloadingOptionsController()
		{
			EditorApplication.playModeStateChanged += OnPlayModeChanged;
		}

		private static void OnPlayModeChanged(PlayModeStateChange state)
		{
			switch (state)
			{
				case PlayModeStateChange.EnteredEditMode:
					break;
				case PlayModeStateChange.ExitingEditMode:
					break;
				case PlayModeStateChange.EnteredPlayMode:
					Debug.Log("<color=cyan>Locking assembly reloading.</color>");
					EditorApplication.LockReloadAssemblies();
					break;
				case PlayModeStateChange.ExitingPlayMode:
					Debug.Log("<color=cyan>Unlocking assembly reloading.</color>");
					EditorApplication.UnlockReloadAssemblies();
					break;
			}
		}

	}

}
