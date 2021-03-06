﻿using UnityEngine;
using System.Collections;

public class LetterSpace : MonoBehaviour {

	public SpriteRenderer boxRenderer;
	public float overAlpha;
	public char validLetter;

	public char ValidLetter { get { return validLetter; } }
	public bool HasValidLetter { get; set; }

	void Start() {
		SetBoxAlpha(0.0f);
		HasValidLetter = false;
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
