using UnityEngine;
using System.Collections;

public class WordChooser : MonoBehaviour {

	public ImageWordGame[] words;
	public float enterTime;
	public Transform selectedPivot;
	public float selectedTime;
	public WordContainer wordContainer;
	public LetterContainer letterContainer;
	public GameEnding gameEnding;

	ImageWordGame selectedWord;

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
		if ( selectedWord == null ) {
			SetMouseInForWord(word);
			if ( Input.GetMouseButtonDown(0) && word != null ) {
				selectedWord = word;
				SetMouseInForWord(null);
				SelectWord(word);
				word.MoveToPivot(selectedPivot.position, selectedTime);
				wordContainer.SetupSpaces(word.wordData);
				letterContainer.SetupLetters(word.wordData);
				word.PlayAudio();
				gameEnding.SelectedWord = word;
			}
		} else {
			if ( Input.GetMouseButtonDown(0) && word != null ) {
				word.PlayAudio();
			}
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
