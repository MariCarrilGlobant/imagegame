using UnityEngine;
using System.Collections;

public class GameEnding : MonoBehaviour {

	public WordContainer wordContainer;
	public LetterContainer letterContainer;
	public AudioSource audioSource;
	public AudioClip cheerSound;
	public GameObject[] confetti;
	public AudioClip confettiSound;
	public float confettiDelay;

	public void CheckForGameEnd() {
		if ( AllLettersInPlace() ) {
			RemoveAllLetters();
			audioSource.PlayOneShot(cheerSound);
			StartCoroutine(LaunchConfetti());
		}
	}

	bool AllLettersInPlace() {
		for ( int i = 0; i < wordContainer.transform.childCount; i++ ) {
			LetterSpace space = wordContainer.transform.GetChild(i).GetComponent<LetterSpace>();
			if ( space != null && ! space.HasValidLetter ) return false;
		}
		return true;
	}

	void RemoveAllLetters() {
		for ( int i = 0; i < letterContainer.transform.childCount; i++ ) {
			LetterBox letter = letterContainer.transform.GetChild(i).GetComponent<LetterBox>();
			if ( letter != null && ! letter.InPosition ) Destroy(letter.gameObject);
		}
	}

	IEnumerator LaunchConfetti() {
		for ( int i = 0; i < confetti.Length; i++ ) {
			yield return new WaitForSeconds(confettiDelay);
			audioSource.PlayOneShot(confettiSound);
			confetti[i].gameObject.SetActive(true);
		}
	}

}
