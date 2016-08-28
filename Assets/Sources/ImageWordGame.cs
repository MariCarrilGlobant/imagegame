using UnityEngine;
using System.Collections;

public class ImageWordGame : MonoBehaviour {

	public WordData wordData;
	public SpriteRenderer mainImageRenderer;
	public Animator animator;
	public Collider2D mainImageCollider;

	void Start() {
		mainImageRenderer.sprite = wordData.mainImage;
	}

	public void EnterWord() {
		animator.SetTrigger("Enter");
	}

	public void SetMouseIn(bool mouseIn) {
		animator.SetBool("MouseIn", mouseIn);
	}

	public void SelectWord() {
		mainImageCollider.enabled = false;
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

}
