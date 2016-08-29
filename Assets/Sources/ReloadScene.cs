using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

	public string sceneName;
	public float reloadDelay;

	float delay;

	void Start() {
		delay = reloadDelay;
	}

	void Update() {
		if ( delay > 0.0f ) {
			delay -= Time.deltaTime;
			if ( delay <= 0.0f ) {
				SceneManager.LoadScene(sceneName);
				delay = 0.0f;
			}
		}
	}

}
