using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

	public void RestartGame() {
		SceneManager.LoadScene("Restart");
	}

}
