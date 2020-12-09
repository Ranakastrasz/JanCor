using UnityEngine;
using UnityEditor;

public class UtilityFunctions
{
	public static void Quit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}