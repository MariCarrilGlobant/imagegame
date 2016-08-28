using UnityEngine;
using System.Collections;

public class WordChooser : MonoBehaviour {

	public ImageWordGame[] words;
	public float enterTime;
	public Transform selectedPivot;
	public float selectedTime;

	void Start() {
		StartCoroutine(EnterAllWords());
	}

	IEnumerator EnterAllWords() {
		for ( int i = 0; i < words.Length; i++ ) {
			yield return new WaitForSeconds(enterTime);
			words[i].EnterWord();
		}
	}

	void Update() {
		ImageWordGame word = WordUnderMouse();
		SetMouseInForWord(word);
		if ( Input.GetMouseButtonDown(0) && word != null ) {
			SetMouseInForWord(null);
			SelectWord(word);
			word.MoveToPivot(selectedPivot.position, selectedTime);
		}
	}

	ImageWordGame WordUnderMouse() {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		return (hit.collider != null) ? hit.collider.GetComponent<ImageWordGame>() : null;
	}

	void SetMouseInForWord(ImageWordGame word) {
		for ( int i = 0; i < words.Length; i++ ) {
			if ( words[i].gameObject.activeSelf ) {
				words[i].SetMouseIn(word == words[i]);
			}
		}
	}

	void SelectWord(ImageWordGame word) {
		for ( int i = 0; i < words.Length; i++ ) {
			if ( words[i] == word ) words[i].SelectWord();
			else words[i].DismissWord();
		}
	}

}
