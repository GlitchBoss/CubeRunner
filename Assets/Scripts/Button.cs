using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour {

	public void PlaySFX()
	{
		try{
			SoundManager.instance.PlayButtonSFX ();
		}
		catch(NullReferenceException e)
		{
			Debug.Log ("Error: " + e.Source);
		}
	}

	public void ToggleAudio(string whatToToggle)
	{
		try
		{
			SoundManager.instance.ToggleAudio (whatToToggle);
		}
		catch(NullReferenceException e)
		{
			Debug.Log ("Error: " + e.Source);
		}
	}

	public void LoadLastLevel()
	{
		int level = PlayerPrefs.GetInt ("LastLevel");
		if(level == null || level == 0)
			level = 1;
		Application.LoadLevel (level + 1);
	}

	public void LoadNextLevel()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void Pause()
	{
		GameManager.instance.TM.Pause ();
	}

	public void Resume()
	{
		GameManager.instance.TM.Resume ();
	}

	public void Retry()
	{
		GameManager.instance.Retry ();
	}
}
