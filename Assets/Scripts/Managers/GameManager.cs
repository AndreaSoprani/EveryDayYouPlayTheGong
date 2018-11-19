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

	[Header("Camera Prefab")]
	public GameObject Camera;
	
	private Scene _currentScene;

	private GameObject _player;
	
	// Use this for initialization
	void Start ()
<<<<<<< HEAD
	{
		SetNewScene(StartScene);
		_player = Instantiate(Player, Vector2.zero, Quaternion.identity);
=======
	{	
		//TODO Change with menu management
		InitNewArea(StartScene);
>>>>>>> 53553f1884dfe6ff4e1def69072e9ea1a0716c03
	}

	/// <summary>
	/// Initialize the first scene where the player starts playing
	/// </summary>
	/// <param name="scene">Name of the scene to start</param>
	void InitNewArea(string scene)
	{
		//TODO Need to change the coordinate of the MC
		Player.SetActive(true);
		//TODO Need to change the coordinate based on MC position
		Camera.SetActive(true);
		SceneManager.LoadScene(scene);
	}
	
	
	
}
