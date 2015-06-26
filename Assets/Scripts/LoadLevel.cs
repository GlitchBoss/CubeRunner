using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {
		
	public GameObject loadingImage;
		
	public void LoadScene(int level)
	{
		loadingImage.SetActive(true);
		Application.LoadLevel(level);
	}

	public void LoadScene(string level)
	{
		loadingImage.SetActive(true);
		Application.LoadLevel(level);
	}
		
	public void LoadAdditive(int level)
	{
		Application.LoadLevelAdditive(level);
	}

	public void LoadAdditive(string level)
	{
		Application.LoadLevelAdditive(level);
	}
}
