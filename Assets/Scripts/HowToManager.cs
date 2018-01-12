using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToManager : MonoBehaviour {
	public Image currentImage;
	enum TutorialImage { tut1, tut2, tut3, tut4, tut5, tut6, tut7 };
	TutorialImage tutImage;
	TutorialImage next;
	// Use this for initialization
	void Awake () {
		GetComponentInParent<Canvas>().enabled = false;
	}

	void Start () {
		tutImage = TutorialImage.tut1;
		next = TutorialImage.tut1;
		nextImage();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showTutorial() {
		GetComponentInParent<Canvas>().enabled = true;
	}

	public void hideTutorial() {
		GetComponentInParent<Canvas>().enabled = false;
	}

	public void nextImage() {
		tutImage = next;
		switch (tutImage) {
			case TutorialImage.tut1:
				currentImage.sprite = Resources.Load<Sprite>("01_Heap");
				next = TutorialImage.tut2;
				break;
			case TutorialImage.tut2:
				currentImage.sprite = Resources.Load<Sprite>("02_NormalWin");
				next = TutorialImage.tut3;
				break;
			case TutorialImage.tut3:
				currentImage.sprite = Resources.Load<Sprite>("03_MisereWin");
				next = TutorialImage.tut4;
				break;
			case TutorialImage.tut4:
				currentImage.sprite = Resources.Load<Sprite>("04_SelectPawn");
				next = TutorialImage.tut5;
				break;
			case TutorialImage.tut5:
				currentImage.sprite = Resources.Load<Sprite>("05_SelectPawnsSingleHeap");
				next = TutorialImage.tut6;
				break;
			case TutorialImage.tut6:
				currentImage.sprite = Resources.Load<Sprite>("06_SelectedPawnsDestroyed_1");
				next = TutorialImage.tut7;
				break;
			case TutorialImage.tut7:
				currentImage.sprite = Resources.Load<Sprite>("07_SelectedPawnsDestroyed_2");
				next = TutorialImage.tut1;
				break;

		}
	}

	public void previousImage() {
		switch (tutImage) {
			case TutorialImage.tut1:
				next = TutorialImage.tut7;
				break;
			case TutorialImage.tut2:
				next = TutorialImage.tut1;
				break;
			case TutorialImage.tut3:
				next = TutorialImage.tut2;
				break;
			case TutorialImage.tut4:
				next = TutorialImage.tut3;
				break;
			case TutorialImage.tut5:
				next = TutorialImage.tut4;
				break;
			case TutorialImage.tut6:
				next = TutorialImage.tut5;
				break;
			case TutorialImage.tut7:
				next = TutorialImage.tut6;
				break;
		}
		nextImage();
	}

}
