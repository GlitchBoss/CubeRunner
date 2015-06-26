using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public GameObject startPanel;
	public Text startText;
	public Text scoreText;
	public Text highScoreText;
	public Text finishScoreText;
	public GameObject finishPanel;
	public float score;
	public float highScore;

	public static GameManager instance;

	bool gameOver = false;
	Spawner spawner;

	void Awake()
	{
		if (instance == null)
			instance = this;
		if (instance != this)
			Destroy (this.gameObject);

		StartUp ();
	}

	void StartUp ()
	{
		if (!startText)
			startText = GameObject.Find ("Start Text").GetComponent<Text> ();
		startText.text = "(Tap To Start)";
		startPanel.SetActive (true);

		Time.timeScale = 0.0f;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;

		if(!scoreText)
			scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		scoreText.text = "Score: 0";

		if(!highScoreText)
			highScoreText = GameObject.Find ("High Score").GetComponent<Text> ();
		highScore = PlayerPrefs.GetFloat ("HighScore");
		highScoreText.text = "High Score: " + (int)(highScore * 100);

		if (!finishPanel)
			finishPanel = GameObject.Find ("FinishPanel");

		if (!finishScoreText)
			finishScoreText = GameObject.Find ("FinishScore").GetComponent<Text> ();

		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetButton ("Jump") && !gameOver) {
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = Time.timeScale * 0.02f;
			startPanel.SetActive (false);
		}
		if (!gameOver) {
			score += Time.deltaTime;
			UpdateText ();
		}
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
}
