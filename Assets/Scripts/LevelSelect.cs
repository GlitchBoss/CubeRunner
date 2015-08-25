using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

	public Button[] btns;
	public GameObject[] lockedImg;

	int levelNum = 1;

	void Start()
	{
		foreach (Button btn in btns) {

			if(PlayerPrefs.GetInt ("Unlocked" + levelNum.ToString ()) == 1 || levelNum == 1)
			{
				btn.interactable = true;
				lockedImg[levelNum - 1].SetActive (false);
			}else{
				if(levelNum != 1)
				{
					btn.interactable = false;
					lockedImg[levelNum - 1].SetActive (true);
				}
			}
			levelNum++;
		}
	}
}
