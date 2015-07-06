using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	GameObject startPanel;
	Text startText;
	Text scoreText;
	Text highScoreText;
	Text finishScoreText;
	GameObject finishPanel;
	public float score;
	public float highScore;
	public AudioSource buttonSFX;
	AudioSource music;
	Toggle musicToggle;
	Toggle sfxToggle;
	GameObject optionsPanel;

	public static GameManager instance;

	public bool gameOver = false;
	Spawner spawner;
	bool gameStarted;
	bool firstTime = true;
	public bool musicOn;
	public bool sfxOn;

	void Awake()
	{
		if (instance == null)
			instance = this;
		if (instance != this)
			Destroy (this.gameObject);

		firstTime = true;
		StartUp ();
	}

	void OnLevelWasLoaded()
	{
		if (!firstTime)
			StartUp ();
	}

	void StartUp ()
	{
		switch(Application.loadedLevel)
		{
		case 0:
			music = GameObject.Find ("Music").GetComponent<AudioSource> ();
			musicToggle = GameObject.Find ("MusicToggle").GetComponent<Toggle>();
			sfxToggle = GameObject.Find ("SFXToggle").GetComponent<Toggle>();
			optionsPanel = GameObject.Find ("Options");
			int on = PlayerPrefs.GetInt ("MusicToggle");
			if(on == 0)
			{
				musicToggle.isOn = true;
				music.Play ();
			}
			else if(on == 1)
			{
				musicToggle.isOn = false;
				music.Stop ();
			}
			else
			{
				musicToggle.isOn = true;
				music.Play();
			}
			musicOn = musicToggle.isOn;
			on = PlayerPrefs.GetInt ("SFXToggle");
			if(on == 0)
			{
				sfxToggle.isOn = true;
			}
			else if(on == 1)
			{
				sfxToggle.isOn = false;
			}
			else
			{
				sfxToggle.isOn = true;
			}
			sfxOn = sfxToggle.isOn;
			optionsPanel.SetActive (false);
			break;
		case 1:
			music = GameObject.Find ("Music").GetComponent<AudioSource> ();

			startPanel = GameObject.Find ("StartPanel");

			startText = GameObject.Find ("Start Text").GetComponent<Text> ();
			startText.text = "(Tap To Start)";
			startPanel.SetActive (true);

			Time.timeScale = 0.0f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;

			scoreText = GameObject.Find ("Score").GetComponent<Text> ();
			scoreText.text = "Score: 0";

			highScoreText = GameObject.Find ("High Score").GetComponent<Text> ();
			highScore = PlayerPrefs.GetFloat ("HighScore");
			highScoreText.text = "High Score: " + (int)(highScore * 100);

			finishPanel = GameObject.Find ("FinishPanel");
			finishPanel.SetActive (false);

			finishScoreText = finishPanel.transform.FindChild ("FinishScore").GetComponent<Text> ();

			spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();

			gameOver = false;
			gameStarted = false;
			music.playOnAwake = false;
			if(musicOn)
				music.Play ();
			break;
		}
		if(firstTime)
			firstTime = false;
	}

	void Update()
	{
		if (Application.loadedLevel == 0)
			return;
		if (CrossPlatformInputManager.GetButton ("Jump") && !gameStarted) {
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			startPanel.SetActive (false);
			music.Play ();
			GameObject.FindGameObjectWithTag ("Player").
				GetComponent<AudioSource>().Play ();
			gameStarted = true;
		}
		if (!gameOver && gameStarted) {
			score += Time.deltaTime;
			UpdateText ();
		}
		GameObject[] p = GameObject.FindGameObjectsWithTag ("Player");
		if (p.Length >= 2)
			Destroy (p [1]);
	}

	void UpdateText ()
	{
		scoreText.text = "Score: " + (int)(score * 100);
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
		Time.fixedDeltaTime = Time.timeScale * 0.02f;
	}

	public void Retry ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ToggleAudio(string whatToToggle)
	{
		switch (whatToToggle) {
		case "Music":
			musicOn = !musicOn;
			musicToggle.isOn = musicOn;
			if(musicOn)
			{
				PlayerPrefs.SetInt ("MusicToggle", 0);
				music.Play ();
			}
			else
			{
				PlayerPrefs.SetInt ("MusicToggle", 1);
				music.Stop ();
			}
			break;
		case "SFX":
			sfxOn = !sfxOn;
			sfxToggle.isOn = sfxOn;
			if(sfxToggle)
				PlayerPrefs.SetInt ("SFXToggle", 0);
			else
				PlayerPrefs.SetInt ("SFXToggle", 1);
			break;
		}
	}

	public void PlaySFX(AudioSource source)
	{
		if (sfxOn)
			source.Play ();
	}

	public void PlayButtonSFX()
	{
		if (sfxOn)
			buttonSFX.Play ();
	}
}
