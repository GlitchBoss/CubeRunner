using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public void PlaySFX()
	{
		GameManager.instance.PlayButtonSFX ();
	}

	public void ToggleAudio(string whatToToggle)
	{
		GameManager.instance.ToggleAudio (whatToToggle);
	}
}
