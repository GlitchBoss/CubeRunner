using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public void PlaySFX()
	{
		SoundManager.instance.PlayButtonSFX ();
	}

	public void ToggleAudio(string whatToToggle)
	{
		SoundManager.instance.ToggleAudio (whatToToggle);
	}

	public void LoadLastLevel()
	{
		int level = PlayerPrefs.GetInt ("LastLevel");
		if(level == null || level == 0)
			level = 1;
		Application.LoadLevel (level + 1);
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
