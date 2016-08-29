using UnityEngine;
using System.Collections;

public class LetterBox : MonoBehaviour {

	public float snapTime;
	public AudioSource audioSource;
	public float maxRotation;
	public float maxSpeed;
	public AudioClip letterLockSound;
	public ParticleSystem lockParticles;
	public float shakeAmplitude;
	public float shakeTime;

	public char ContainedLetter {
		get { return GetComponentInChildren<TextMesh>().text[0]; }
		set { GetComponentInChildren<TextMesh>().text = value.ToString(); }
	}

	bool grabbed = false;
	Vector3 snapPosition;
	LetterSpace overSpace;
	bool inPosition = false;
	float lastPos;

	void Start() {
		snapPosition = transform.position;
		lastPos = transform.position.x;
	}

	void Update() {
		if ( grabbed && ! Input.GetMouseButton(0) ) ReleaseLetter();
		if ( grabbed ) ClampToMouse();
		float dx = Mathf.Clamp(transform.position.x - lastPos, -maxSpeed, maxSpeed);
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, dx / maxSpeed * maxRotation);
		lastPos = transform.position.x;
	}

	public void GrabLetter() {
		if ( ! inPosition ) {
			grabbed = true;
			Vector3 pos = transform.position;
			pos.z = -1.0f;
			transform.position = pos;
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
			transform.position = pos;
			yield return null;
		}
		pos = transform.position;
		pos.z = 0.0f;
		transform.position = pos;
		if ( inPosition ) StartCoroutine(LockEffect());
	}

	IEnumerator LockEffect() {
		audioSource.PlayOneShot(letterLockSound);
		lockParticles.Play();
		float t = 0.0f;
		Vector3 pos = Camera.main.transform.position;
		while ( t < shakeTime ) {
			t += Time.deltaTime;
			Camera.main.transform.position = pos + Random.insideUnitSphere * shakeAmplitude;
			yield return null;
		}
		Camera.main.transform.position = pos;
	}

	void OnTriggerStay2D(Collider2D other) {
		LetterSpace space = other.GetComponent<LetterSpace>();
		if ( space != null && space != overSpace ) {
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
