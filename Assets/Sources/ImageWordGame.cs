using UnityEngine;
using System.Collections;

public class ImageWordGame : MonoBehaviour {

	public WordData wordData;
	public SpriteRenderer mainImageRenderer;
	public Animator animator;
	public Collider2D mainImageCollider;
	public AudioSource audioSource;
	public AudioClip enterSound;

	void Start() {
		mainImageRenderer.sprite = wordData.mainImage;
		audioSource.clip = wordData.audioClip;
	}

	public void EnterWord() {
		animator.SetTrigger("Enter");
		audioSource.PlayOneShot(enterSound);
	}

	public void SetMouseIn(bool mouseIn) {
		animator.SetBool("MouseIn", mouseIn);
	}

	public void SelectWord() {
		animator.SetTrigger("Select");
	}

	public void DismissWord() {
		mainImageCollider.enabled = false;
		gameObject.SetActive(false);
	}

	public void MoveToPivot(Vector3 endPos, float time) {
		StartCoroutine(DoMoveToPivot(endPos, time));
	}

	IEnumerator DoMoveToPivot(Vector3 endPos, float time) {
		Vector3 startPos = transform.position;
		float t = 0.0f;
		while ( t <= 1.0f ) {
			t += Time.deltaTime / time;
			transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}
	}

	public void PlayAudio() {
		audioSource.Play();
	}

}
