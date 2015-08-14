using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public float score;
	public float highScore;
	public bool gameOver = false;
	public SoundManager SM;

	GameObject startPanel;
	Text startText;
	Text scoreText;
	Text highScoreText;
	Text finishScoreText;
	GameObject finishPanel;
	public Transform player;
	float distance;
	Transform startPoint;
	Spawner spawner;
	bool gameStarted;

	public static GameManager instance;

	void Awake()
	{
		Debug.Log ("Awake");
		if (instance == null) {
			instance = this;
			StartUp ();
		}
		if (instance != this)
			Destroy (this.gameObject);
	}

	void OnLevelWasLoaded(int level)
	{
		Debug.Log ("OnLevelWasLoaded");
		StartUp ();
	}

	void StartUp ()
	{
		Debug.Log ("StartUp");
		SM = GetComponent<SoundManager> ();
		SM.SetUp ();
		if (Application.loadedLevel == 0)
			return;

		GetAndSetupReferences ();
		Time.timeScale = 0.0f;
		Time.fixedDeltaTime = 0.0f;
		gameOver = false;
		gameStarted = false;
	}

	void GetAndSetupReferences()
	{
		Debug.Log ("Loading...");
		startPanel = GameObject.Find ("StartPanel");
		startText = GameObject.Find ("Start Text").GetComponent<Text> ();
		startText.text = "(Tap To Start)";
		startPanel.SetActive (true);
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		scoreText.text = "Score: 0";
		highScoreText = GameObject.Find ("High Score").GetComponent<Text> ();
		highScore = PlayerPrefs.GetFloat ("HighScore");
		highScoreText.text = "High Score: " + (int)(highScore * 100);
		finishPanel = GameObject.Find ("FinishPanel");
		finishPanel.SetActive (false);
		finishScoreText = finishPanel.transform.FindChild ("FinishScore").GetComponent<Text> ();
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		startPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;
		distance = Vector3.Distance (player.position, startPoint.position);
		Debug.Log ("Loaded");
	}

	void Update()
	{
		if (Application.loadedLevel == 0)
			return;
		if ((Input.touches.Length > 0 || Input.GetButtonDown ("Jump")) && !gameStarted) {
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 0.02f;
			startPanel.SetActive (false);
			if(SM.musicOn)
				SM.music.Play ();
			GameObject.FindGameObjectWithTag ("Player").
				GetComponent<AudioSource>().Play ();
			gameStarted = true;
		}
		GameObject[] p = GameObject.FindGameObjectsWithTag ("Player");
		if (p.Length >= 2) {
			Destroy (p [1]);
			if(!player)
				player = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		if (!gameOver && gameStarted) {
			UpdateScore();
		}
	}

	void UpdateScore ()
	{
		Debug.Log ("Updating Score");
		if (player) {
			distance = Vector3.Distance (player.position, startPoint.position);
			Debug.Log ("Distance recalculated");
		}
		UpdateText ();
	}

	void UpdateText ()
	{
//		scoreText.text = "Score: " + (int)(score * 100);
		scoreText.text = "Score: " + (int)(distance * 100);
	}

	public void GameOver()
	{
		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetFloat ("HighScore", highScore);
			Analytics.CustomEvent ("NewHighScore", new Dictionary<string, object>{
				{ "highScore", highScore }
			});
		}

		Analytics.CustomEvent ("GameOver", new Dictionary<string, object>
		                       {
			{ "lastColumn", spawner.previousColumn },
			{ "score", ((int)score * 100)}
		});

		gameOver = true;
		finishPanel.SetActive (true);
		finishScoreText.text = "Your Score: " + (int)(score * 100) +
			"\n\nHigh Score: " + (int)(highScore * 100);
		Time.timeScale = 0.0f;
		Time.fixedDeltaTime = 0.0f;
	}

	public void Retry ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
