using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	public bool forceSingleInstance;

	static DontDestroy instance;

	void Awake () {
		if (forceSingleInstance) {
			if (instance == null)
				instance = this;
			if (instance != this)
				Destroy (this.gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}
}