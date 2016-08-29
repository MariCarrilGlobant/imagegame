using UnityEngine;
using System.Collections;

public class LetterBox : MonoBehaviour {

	public float snapTime;
	public AudioSource audioSource;

	public char ContainedLetter {
		get { return GetComponentInChildren<TextMesh>().text[0]; }
		set { GetComponentInChildren<TextMesh>().text = value.ToString(); }
	}

	bool grabbed = false;
	Vector3 snapPosition;
	LetterSpace overSpace;
	bool inPosition = false;

	void Start() {
		snapPosition = transform.position;
	}

	void Update() {
		if ( grabbed && ! Input.GetMouseButton(0) ) ReleaseLetter();
		if ( grabbed ) ClampToMouse();
	}

	public void GrabLetter() {
		if ( ! inPosition ) {
			grabbed = true;
		}
		audioSource.Play();
	}

	public void ReleaseLetter() {
		grabbed = false;
		if ( overSpace != null ) CheckIfValidSpace();
		MoveToPosition(snapPosition);
	}

	void CheckIfValidSpace() {
		if ( overSpace.ValidLetter == ContainedLetter ) {
			snapPosition = overSpace.transform.position;
			inPosition = true;
		}
	}

	void ClampToMouse() {
		Vector3 pos = Input.mousePosition;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		transform.position = Camera.main.ScreenToWorldPoint(pos);
	}

	void MoveToPosition(Vector3 pos) {
		StartCoroutine(DoMoveToPosition(pos));
	}

	IEnumerator DoMoveToPosition(Vector3 endPos) {
		Vector3 startPos = transform.position;
		Vector3 pos = startPos;
		float t = 0.0f;
		while ( t <= 1.0f ) {
			t += Time.deltaTime / snapTime;
			pos.x = Mathf.SmoothStep(startPos.x, endPos.x, t);
			pos.y = Mathf.SmoothStep(startPos.y, endPos.y, t);
			pos.z = Mathf.SmoothStep(startPos.z, endPos.z, t);
			transform.position = pos;
			yield return null;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		LetterSpace space = other.GetComponent<LetterSpace>();
		if ( space != null ) {
			if ( overSpace != null ) overSpace.ExitLetter();
			overSpace = space;
			space.EnterLetter();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		LetterSpace space = other.GetComponent<LetterSpace>();
		if ( space != null ) {
			if ( overSpace == space ) overSpace = null;
			space.ExitLetter();
		}
	}

}
