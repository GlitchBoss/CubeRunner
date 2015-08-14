using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	public AudioSource buttonSFX;
	public AudioSource music;
	public Toggle musicToggle;
	public Toggle sfxToggle;
	public bool musicOn;
	public bool sfxOn;

	GameObject optionsPanel;

	public static SoundManager instance;

	void Awake()
	{
		if (instance == null) {
			instance = this;
//			Setup ();
		}
		if (instance != this)
			Destroy (this.gameObject);
	}

	public void SetUp ()
	{
		music = GameObject.Find ("Music").GetComponent<AudioSource> ();
		buttonSFX = GetComponent<AudioSource> ();
		if (Application.loadedLevel == 0) {
			musicToggle = GameObject.Find ("MusicToggle").GetComponent<Toggle> ();
			sfxToggle = GameObject.Find ("SFXToggle").GetComponent<Toggle> ();
			optionsPanel = GameObject.Find ("Options");
			optionsPanel.SetActive (false);
		}
		GetAudioPrefs ();
	}

	void GetAudioPrefs()
	{
		int on = PlayerPrefs.GetInt ("MusicToggle");
		if(on == 0)
		{
			musicOn= true;
			music.Play();
		}
		else
		{
			musicOn = false;
			music.Stop ();
		}
		if(Application.loadedLevel == 0)
			musicToggle.isOn = musicOn;

		on = PlayerPrefs.GetInt ("SFXToggle");
		if(on == 0)
		{
			sfxOn = true;
		}
		else
		{
			sfxOn = false;
		}
		if(Application.loadedLevel == 0)
			sfxToggle.isOn = sfxOn;
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
			if(sfxOn)
				PlayerPrefs.SetInt ("SFXToggle", 0);
			else
				PlayerPrefs.SetInt ("SFXToggle", 1);
			break;
		}
	}

	public void PlayButtonSFX()
	{
		if (sfxOn)
			buttonSFX.Play ();
	}

	public void PlaySFX(AudioSource sfx)
	{
		if (sfxOn)
			sfx.Play ();
	}
}
