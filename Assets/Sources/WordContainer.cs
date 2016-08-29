using UnityEngine;
using System.Collections;

public class WordContainer : MonoBehaviour {

	public LetterSpace letterSpacePrefab;
	public Vector3 startPos;
	public Vector3 spawnSpacing;

	public void SetupSpaces(WordData wordData) {
		startPos.x = - spawnSpacing.x * ( wordData.word.Length - 1 ) / 2;
		for ( int i = 0; i < wordData.word.Length; i++ ) {
			LetterSpace space = Instantiate<LetterSpace>(letterSpacePrefab);
			space.transform.SetParent(transform);
			space.validLetter = wordData.word[i];
			space.transform.position = startPos + spawnSpacing * i;
		}
	}

}
