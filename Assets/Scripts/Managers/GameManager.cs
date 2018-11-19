using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	[Header("Scenes")]
	public string StartScene;

	[Header("Player Prefab")]
	public GameObject Player;

	private Scene _currentScene;

	private GameObject _player;
	
	// Use this for initialization
	void Start ()
	{
		SetNewScene(StartScene);
		_player = Instantiate(Player, Vector2.zero, Quaternion.identity);
	}

	/// <summary>
	/// Changes the current scene.
	/// </summary>
	/// <param name="scene">the scene to load</param>
	/// <param name="playerPosition">the position of the player in the new scene</param>
	void ChangeScene(string scene, Vector2 playerPosition)
	{
		string oldScene = _currentScene.name;
		SetNewScene(scene);
		_player.transform.position = playerPosition;
		//TODO save and destroy previous scene
		
	}

	void SetNewScene(string scene)
	{
		SceneManager.LoadScene(scene);
		_currentScene = SceneManager.GetSceneByName(scene);
		SceneManager.SetActiveScene(_currentScene);
	}
	
	
	
}
