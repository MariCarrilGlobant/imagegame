using UnityEngine;
using System.Collections;

public class LetterContainer : MonoBehaviour {

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

}
