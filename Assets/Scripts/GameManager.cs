using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class GameManager : MonoBehaviour {

	public float score;
	public float highScore;
	public bool gameOver = false;
	public SoundManager SM;
	public TimeManager TM;
	public float time;
	public float bestTime;

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
	Level currentLevel;

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
		TM = GetComponent<TimeManager> ();
		if (Application.loadedLevel == 0)
			return;

		GetAndSetupReferences ();
		Time.timeScale = 0.0f;
		Time.fixedDeltaTime = 0.0f;
		gameOver = false;
		gameStarted = false;
		score = 0;
	}

	void GetAndSetupReferences()
	{
		Debug.Log ("Loading...");
		startPanel = GameObject.Find ("StartPanel");
		startText = GameObject.Find ("Start Text").GetComponent<Text> ();
		startText.text = "(Tap To Start)";
		startPanel.SetActive (true);
		finishPanel = GameObject.Find ("FinishPanel");
		finishPanel.SetActive (false);
		finishScoreText = finishPanel.transform.FindChild ("FinishScore").GetComponent<Text> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		if (Application.loadedLevel == 1) {
			scoreText = GameObject.Find ("Score").GetComponent<Text> ();
			scoreText.text = "Score: 0";
			highScoreText = GameObject.Find ("High Score").GetComponent<Text> ();
			highScore = PlayerPrefs.GetFloat ("EndlessHighScore");
			highScoreText.text = "High Score: " + (int)(highScore * 100);
			spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
			startPoint = GameObject.FindGameObjectWithTag ("StartPoint").transform;
			distance = Vector3.Distance (player.position, startPoint.position);
		} else if (Application.loadedLevel >= 2) {
			currentLevel = GameObject.Find ("Level").GetComponent<Level>();

		}

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

		}
		if (p.Length >= 1) {
			if(!player)
				player = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		if (!gameOver && gameStarted && player != null) {
			UpdateScore();
		}
	}

	void UpdateScore ()
	{
		Debug.Log ("Updating Score");
		if (Application.loadedLevel == 1) {
//			if (player) {
//				distance = Vector3.Distance (player.position, startPoint.position);
//				Debug.Log ("Distance recalculated");
//			}
			score += Time.deltaTime;
		} else if (Application.loadedLevel >= 2) {
			time += Time.deltaTime;
		}
		UpdateText ();
	}

	void UpdateText ()
	{
		if (Application.loadedLevel == 1) {
			scoreText.text = "Score: " + (int)(score * 100);
//			scoreText.text = "Score: " + (int)(distance * 100);
		}
	}

	public void GameOver()
	{
		if (Application.loadedLevel == 1) {
			if (score > highScore) {
				highScore = score;
				PlayerPrefs.SetFloat ("EndlessHighScore", highScore);
				Analytics.CustomEvent ("NewHighScore", new Dictionary<string, object>{
				{ "highScore", highScore }
			});
			}
			Analytics.CustomEvent ("GameOver", new Dictionary<string, object>
			                       {
				{ "lastColumn", spawner.previousColumn },
				{ "score", ((int)score * 100)}
			});
		}
		else if (Application.loadedLevel >= 2) {
			bestTime = PlayerPrefs.GetFloat ("HighScore" + currentLevel.level.ToString ());
			if(time > bestTime)
			{
				bestTime = time;
				PlayerPrefs.SetFloat ("HighScore" + currentLevel.level.ToString (), bestTime);
			}
		}

		gameOver = true;
		finishPanel.SetActive (true);
		if (Application.loadedLevel == 1) {
			finishScoreText.text = "Your Score: " + (int)(score * 100) +
				"\n\nHigh Score: " + (int)(highScore * 100);
		} else if (Application.loadedLevel >= 2) {
			finishScoreText.text = String.Format ("Your Time: {0}\n\nBest Time: {1}",
			                                      Math.Round (time, 2),
			                                      Math.Round (bestTime, 2));
		}
		Time.timeScale = 0.0f;
		Time.fixedDeltaTime = 0.0f;
	}

	public void Retry ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
