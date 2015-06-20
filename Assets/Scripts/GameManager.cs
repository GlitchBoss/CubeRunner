using UnityEngine;
using System.Collections;

using UnityEngine.UI;
//using UnityEngine.Analytics;
//using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
	public float score;
	public float highScore;

	public static GameManager instance;

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
		if(!scoreText)
			scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		scoreText.text = "Score: 0";

		if(!highScoreText)
			highScoreText = GameObject.Find ("High Score").GetComponent<Text> ();
		highScore = PlayerPrefs.GetFloat ("HighScore");

		highScoreText.text = "High Score: " + ((int)highScore * 100);

		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
	}

	void Update()
	{
		score += Time.deltaTime;
		UpdateText ();
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
//			Analytics.CustomEvent ("NewHighScore", new Dictionary<string, object>{
//				{ "highScore", highScore }
//			});
		}

//		Analytics.CustomEvent ("GameOver", new Dictionary<string, object>
//		                       {
//			{ "lastColumn", spawner.previousColumn },
//			{ "score", ((int)score * 100)}
//		});

		Application.LoadLevel (Application.loadedLevel);
	}
}
