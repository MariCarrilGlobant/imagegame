using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LetterContainer : MonoBehaviour {

	public LetterBox letterBoxPrefab;
	public Vector3 startPos;
	public Vector3 spawnSpacing;
	public int letterCount;
	public AudioData[] audioData;

	[System.Serializable]
	public class AudioData {
		public char ch;
		public AudioClip clip;
	}

	void Update() {
		CheckForLetterGrab();
	}

	void CheckForLetterGrab() {
		if ( Input.GetMouseButtonDown(0) ) {
			LetterBox letter = LetterUnderMouse();
			if ( letter != null ) {
				letter.GrabLetter();
			}
		}
	}

	LetterBox LetterUnderMouse() {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		return (hit.collider != null) ? hit.collider.GetComponent<LetterBox>() : null;
	}

	public void SetupLetters(WordData wordData) {
		List<LetterBox> letters = new List<LetterBox>();
		for ( int i = 0; i < wordData.word.Length; i++ ) {
			letters.Add(NewLetter(wordData.word[i]));
		}
		while ( letters.Count < letterCount ) {
			letters.Add(NewLetter(wordData.word[Random.Range(0, wordData.word.Length)]));
		}
		int idx = 0;
		while ( letters.Count > 0 ) {
			int pos = Random.Range(0, letters.Count);
			LetterBox letter = letters[pos];
			letters.RemoveAt(pos);
			letter.transform.position = startPos + spawnSpacing * (idx++);
		}
	}

	LetterBox NewLetter(char c) {
		LetterBox letter = Instantiate<LetterBox>(letterBoxPrefab);
		letter.transform.SetParent(transform);
		letter.ContainedLetter = c;
		letter.audioSource.clip = null;
		for ( int i = 0; i < audioData.Length; i++ ) {
			if ( audioData[i].ch == c ) {
				letter.audioSource.clip = audioData[i].clip;
				break;
			}
		}
		return letter;
	}

}
