using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using UnityEngine.SceneManagement;

namespace Wipeout
{
	public class SceneLoader : MonoBehaviour
	{
		public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		public void QuitGame()
		{
		#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
		#else
	    Application.Quit();
		#endif
		}

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				SceneManager.LoadScene("Menu");
			}
		}
	}
}