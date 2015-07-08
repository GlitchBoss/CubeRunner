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
}
