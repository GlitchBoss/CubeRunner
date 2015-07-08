using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public void PlaySFX()
	{
		AudioManager.instance.PlayButtonSFX ();
	}

	public void ToggleAudio(string whatToToggle)
	{
		AudioManager.instance.ToggleAudio (whatToToggle);
	}
}
