using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialOnClick : MonoBehaviour {

	public Button tutorialButton;
	public GameObject tutorialImage;
	public Button backButton;

	void Start() {
		tutorialButton.onClick.AddListener (onTutorialButtonClick);
		backButton.onClick.AddListener (onBackButtonClick);
		backButton.gameObject.SetActive (false);
		tutorialImage.gameObject.SetActive (false);
	}

	public void onTutorialButtonClick() {
		tutorialImage.gameObject.SetActive (true);
		backButton.gameObject.SetActive (true);
	}

	public void onBackButtonClick() {
		tutorialImage.gameObject.SetActive (false);
		backButton.gameObject.SetActive (false);
	}
}
