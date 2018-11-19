using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader Instance { get; private set; }

	private List<string> _sceneNames;

	private string _currentScene;
	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			_sceneNames = new List<string>();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	/// <summary>
	/// Start the coroutine that loads adjacent scenes
	/// </summary>
	/// <param name="currentScene">The scene the player has entered</param>
	/// <param name="sceneNames">The scene adjacent to the new scene</param>
	public void LoadNewScenes(string currentScene, List<string> sceneNames)
	{
		_currentScene = currentScene;
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentScene));
		StartCoroutine(UpdateLoadedScenes(sceneNames));
	}

	/// <summary>
	/// Load new adjacent scenes and unload unreachable scenes
	/// </summary>
	/// <param name="sceneNames">List of adjacent scenes of the new scene</param>
	/// <returns></returns>
	private IEnumerator UpdateLoadedScenes(List<string> sceneNames)
	{
		//TODO Need to fix a problem: when the MC pass around triggers too quickly, some scene may not be unloaded properly.
		//Possible solution#1: make the trigger smaller than the actual scene so to leave a gap to allow the scenes to be properly unloaded.
		//Possible solution#2: create queue of coroutines. This solution may show weird behaviour unless a way of checking the situation is introduced.
		List<string> toDelete = _sceneNames.Except(sceneNames).ToList();
		_sceneNames = sceneNames;

		toDelete.Remove(_currentScene);
		
		Debug.Log("Number of scene to delete: " + toDelete.Count);
		for(int i = 0; i < toDelete.Count; i++)
		{
			Debug.Log("Unloading: " + toDelete[i]);
			SceneManager.UnloadSceneAsync(toDelete[i]);
			yield return null;
		}

		for (int i = 0; i < _sceneNames.Count; i++)
		{
			if (!SceneManager.GetSceneByName(sceneNames[i]).isLoaded)
			{
				SceneManager.LoadSceneAsync(sceneNames[i], LoadSceneMode.Additive);
			}

			yield return null;
		}

		
		
	}
}

