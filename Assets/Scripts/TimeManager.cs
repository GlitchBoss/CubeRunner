using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public void Pause()
	{
		Time.timeScale = 0.0f;
		Time.fixedDeltaTime = 0.0f;
	}

	public void Resume()
	{
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f;
	}
}
