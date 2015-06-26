using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {
	
	public AudioClip otherMusic;

	AudioSource source;

	void Awake () 
	{
		source = GetComponent<AudioSource>();
	}
	
	
	void OnLevelWasLoaded(int level)
	{
		if (level == 2)
		{
			source.clip = otherMusic;
			source.Play ();
		}
		
	}
}