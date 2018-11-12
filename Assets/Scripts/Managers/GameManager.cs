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
	public Collection<Scene> Scenes;
	public Scene StartScene;

	[Header("Player Prefab")]
	public GameObject Player;

	private Scene _currentScene;

	private GameObject _player;
	
	private EventManager _eventManager;
	private SoundManager _soundManager;
	
	// Use this for initialization
	void Start ()
	{
		_currentScene = StartScene;
		SceneManager.SetActiveScene(_currentScene);
		_player = Instantiate(Player, Vector2.zero, Quaternion.identity);
	}

	/// <summary>
	/// Changes the current scene.
	/// </summary>
	/// <param name="scene">the scene to load</param>
	/// <param name="playerPosition">the position of the player in the new scene</param>
	void ChangeScene(Scene scene, Vector2 playerPosition)
	{
		if (!Scenes.Contains(scene)) return;
		_currentScene = scene;
		SceneManager.SetActiveScene(_currentScene);
		_player.transform.position = playerPosition;
		
	}
	
	
}
