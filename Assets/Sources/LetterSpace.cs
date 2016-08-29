using UnityEngine;
using System.Collections;

public class LetterSpace : MonoBehaviour {

	public SpriteRenderer boxRenderer;
	public float overAlpha;
	public char validLetter;

	public char ValidLetter { get { return validLetter; } }

	void Start() {
		SetBoxAlpha(0.0f);
	}

	public void EnterLetter() {
		SetBoxAlpha(overAlpha);
	}

	public void ExitLetter() {
		SetBoxAlpha(0.0f);
	}

	void SetBoxAlpha(float a) {
		Color c = boxRenderer.color;
		c.a = a;
		boxRenderer.color = c;
	}

}
