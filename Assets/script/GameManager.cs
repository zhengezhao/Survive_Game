using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static float hunger = 50.0f;
	public static float thirst = 50.0f;
	public Sprite gameover;

	public Button useBtn;
	public Button searchBtn;
	public Text diary;

	public Text hungerText;
	public Text thirstText;
	public static float startTime=0.0f;
	public static float totalTime = 16.0f;
	public static float voidSearchTime = 0.5f;
	public static float invalidSearchThirst = 1.0f;
	public static float invalidSearchHunger = 1.0f;

	public Text dayText;
	public static int day = 1;

	public GameObject selectedObject;
	public static bool buttonClicked = false;

	Gradient timeBarColor = new Gradient();
	public Image timeBarImage;

	Gradient energyBarColor = new Gradient();
	public Image energyBar;

	Gradient hydraBarColor = new Gradient();
	public Image hydraBar;

	public RaycastHit hitInfoTmp;

	public static AudioSource validUse;
	public static AudioSource invalidUse;
	public static AudioSource background;
	public static AudioSource bookFlop;
	public static AudioSource gameoverSound;
	public static AudioSource winSound;
	public static AudioSource rainSound;
	public static AudioSource cover1;
	public static AudioSource cover3;
	public static AudioSource goodEnding;
	public static AudioSource badEnding;

	public Button expandDiaryBtn;
	public Button closeExpandBtn;
	public GameObject expandDiaryPanel;
	private bool isOpen = false;
	public Text expandText;

	private string dayTextStr = "";

	public GameObject nextDayButtonGO;
	public Button nextDayButton;

	public Button backToMenuBtn;

	public bool endingExecuted;
	public int endType = 0;

	public static int gameTime = 0;

	// 1:pos 0: neu -1:neg
	public static int LifeStatus(){
		if (hunger > 15 && thirst > 15) {
			return -1;
		} else if (hunger < 10 && thirst < 10) {
			return 1;
		} else{
			return 0;
		}
	}

	// Use this for initialization
	void Start () {


		AudioSource[] allSource = GetComponents<AudioSource>();
		validUse = allSource [0];
		invalidUse = allSource [1];
		background = allSource [2];
		bookFlop = allSource [3];
		gameoverSound = allSource [4];
		winSound = allSource [5];
		rainSound = allSource [6];
		cover1 = allSource [7];
		cover3 = allSource [8];
		goodEnding = allSource [9];
		badEnding = allSource [10];

		if (gameTime == 0) {
			StartCoroutine (gameStart ());
		}

		if (gameTime != 0) {
			background.Play ();
		}

		selectedObject = GameObject.Find("Dummy");

		useBtn.onClick.AddListener (onUseButtonClick);
		searchBtn.onClick.AddListener (onSearchButtonClick);
		expandDiaryBtn.onClick.AddListener (onExpandDiaryButtonClick);
		closeExpandBtn.onClick.AddListener (onCloseExpandButtonClick);
		nextDayButton.onClick.AddListener (onNextDayButtonClick);
		backToMenuBtn.onClick.AddListener (onbackToMenuBtnClick);

		timeBarColor.SetKeys (
			new GradientColorKey[] { new GradientColorKey (Color.white, 0.0f), new GradientColorKey (Color.red, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (1.0f, 0.0f), new GradientAlphaKey (1.0f, 1.0f) });

		energyBarColor.SetKeys (
			new GradientColorKey[] { new GradientColorKey (new Color32(63, 191, 63, 1), 1.0f), new GradientColorKey (Color.red, 0.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (1.0f, 0.0f), new GradientAlphaKey (1.0f, 1.0f) });

		hydraBarColor.SetKeys (
			new GradientColorKey[] { new GradientColorKey (new Color32(63, 191, 63, 1), 1.0f), new GradientColorKey (Color.red, 0.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (1.0f, 0.0f), new GradientAlphaKey (1.0f, 1.0f) });

		expandDiaryPanel.gameObject.SetActive (false);
		nextDayButtonGO.gameObject.SetActive (false);

		// reset all static variabels (reload scene)

		//game manager
		startTime = 0.0f;
		totalTime = 16.0f;
		voidSearchTime = 0.5f;
		invalidSearchThirst = 1.0f;
		invalidSearchHunger = 1.0f;

		hunger = 50.0f;
		thirst = 50.0f;

		day = 1;
		buttonClicked = false;

		// diary
		Diary_milestone.today_milestone = 0;
		Diary_milestone.day_diary = 1;
		Diary_milestone.diary_text = "\n  Where am I? I don't know.It seems that I am still alive. God bless me. " +
			"I can survive from such a devastating shipwreck! Where am I? I don't know. " +
			"Hopefully it is not the helll. It seems to be a deserted island. I need to " +
			"get some food and drink first. And I still a bonfire to warm my frozen body.";
		Diary_milestone.diary_text += "\n \n[Day " + GameManager.day.ToString()+"]\n";

		diary.text = Diary_milestone.diary_text;

		// inventory
		Inventory.selectedItems.Clear();
		Inventory.inventory.Clear ();

		Inventory.foundApple = false;
		Inventory.foundMeat = false;
	}


	void Update () {

		// UI update
		hungerText.text = "Energy: " + (100 - hunger).ToString ();
		thirstText.text = "Hydration: " + (100 - thirst).ToString ();

		dayText.text = "Day " + day.ToString();

		timeBarImage.rectTransform.localScale = new Vector3 (startTime / 16.0f, 1.0f, 1.0f);
		timeBarImage.color = timeBarColor.Evaluate (startTime / 16.0f);

		energyBar.rectTransform.localScale = new Vector3 (1.0f - hunger / 100.0f, 1.0f, 1.0f);
		energyBar.color = energyBarColor.Evaluate (1.0f - hunger / 100.0f);

		hydraBar.rectTransform.localScale = new Vector3 (1.0f - thirst / 100.0f, 1.0f, 1.0f);
		hydraBar.color = hydraBarColor.Evaluate (1.0f - thirst / 100.0f);


		// success condition
		// good ending
		if (Inventory.inventory.ContainsKey("Raft") && Inventory.inventory.ContainsKey("Apple") && Inventory.inventory["Apple"] >= 4 && !endingExecuted) {
			endingExecuted = true;
			endType = 3;
			gameTime++;
			Diary_milestone.diary_text += "\n This is a story of a strong man who survived from the a disaster. I always trust in myself and I never give up. I have " +
			"a wonderful plan for each of my movements and I thought carefully. I carried enough food with me and finally I was rescued by a passing ship after " +
			"drifting on the sea for a week. People were surprised by what I have done and called a miracle.";
			Diary_milestone.storeDiary ();
			StartCoroutine (goodEndingEffect ());
		}
		// bad ending
		else if (Inventory.inventory.ContainsKey("Raft") && !endingExecuted) {
			endingExecuted = true;
			endType = 2;
			gameTime++;
			Diary_milestone.diary_text += "\nI am so hungry. Probably I should save some apples for this long drift. After three days drifting on the sea. I am going to die." +
				"\n Jesus...Help me .." +
				"\n.....";
			Diary_milestone.storeDiary ();
			StartCoroutine (badEndingEffect ());
		}
		// fail condition
		if ((100 - hunger <= 0 || 100 - thirst <= 0) && !endingExecuted)  {
			endingExecuted = true;
			GameObject go = GameObject.Find ("gameover");
			go.GetComponent<Image> ().enabled = true;
			badEnding.Play ();
			endType = 1;
			gameTime++;
			Diary_milestone.diary_text += "\n I can't surive on this island. I am in bad condition. I am going to die. \n...";
			Diary_milestone.storeDiary ();
			StartCoroutine (effectGameover (go));
		}

		// rain event
		GameObject.Find("EventManager").GetComponent<EventManager>().rainEvent();

		// day alternation
		if (startTime >= 16) {
			startTime = startTime - 16;
			day++;
		}


		// player behavior
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () == false) {

			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hitInfo;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
					selectedObject = hitInfo.transform.gameObject;
					GameObject.Find("Label").GetComponent<Image>().enabled = true;
					GameObject.Find("Label").transform.position = Input.mousePosition;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)
			|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
			GameObject.Find ("Label").GetComponent<Image> ().enabled = false;
			selectedObject = GameObject.Find ("Dummy");
		}

		//update diary
		if (Diary_milestone.day_diary != day){

			int index = 0;
			if (day <= 2) {
				string tmpStr = "\n \n[Day" + (Diary_milestone.day_diary).ToString () + "]\n";
				index = diary.text.Length - tmpStr.Length;
			} else {
				string tmpStr = "\n \n[Day" + (Diary_milestone.day_diary).ToString () + "]\n";
				index = diary.text.Length - tmpStr.Length;
			}


			if (Diary_milestone.today_milestone > 0) {
				Diary_milestone.pos_sen_IEumerator.MoveNext ();
				Diary_milestone.diary_text += Diary_milestone.pos_sen_IEumerator.Current;
				//ayTextStr += Diary_milestone.pos_sen_IEumerator.Current.ToString ();
			} else {
				Diary_milestone.neg_sen_IEumerator.MoveNext ();
				Diary_milestone.diary_text += Diary_milestone.neg_sen_IEumerator.Current;
				//dayTextStr += Diary_milestone.pos_sen_IEumerator.Current.ToString ();
			}

			Diary_milestone.today_milestone = 0;
			dayTextStr = Diary_milestone.diary_text.Substring (index);
			Diary_milestone.diary_text += "\n \n[Day" + (Diary_milestone.day_diary + 1).ToString()+"]\n";

			diary.text = Diary_milestone.diary_text;

			int len = diary.text.Length;

			Diary_milestone.day_diary = day;



			Image tmp = GameObject.Find ("night").GetComponent<Image>();

			if (endType == 0) {
				StartCoroutine (lightChange1 (tmp, dayTextStr));
			}
		}

		expandText.text = diary.text;
	}


	public void onUseButtonClick() {

		int size = Inventory.selectedItems.Count;

		if (size == 1) {
			string tmpName = Inventory.selectedItems [0];
			Inventory.UseItem (tmpName);

			if (tmpName == "DiaryBook") {
				GameObject.Find ("DiaryText").GetComponent<Text> ().enabled = true;
				StartCoroutine (effect3 ());
			}

			if (ItemManager.ItemCombo.ContainsKey (tmpName)) {
				HashSet<string> set = ItemManager.ItemCombo [tmpName];
				foreach (string str in set) {
					GameObject go = new GameObject ("New Sprite");
					SpriteRenderer renderer = go.AddComponent<SpriteRenderer> ();
					renderer.sprite = ItemManager.Itemlist [str].itemImage;
					go.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 20.0f));
					go.transform.LookAt (Camera.main.transform);
					StartCoroutine (effect (go, tmpName));
				}
			} 
//			else {
//				GameManager.hunger += ItemManager.getItem (tmpName).item_hunger;
//				GameManager.thirst += ItemManager.getItem (tmpName).item_thirst;
//				GameManager.startTime += 1;
//			}

		} else if (size > 1) {
			HashSet<string> set = Inventory.makeItem ();
			if (set != null && set.Count == 1) {
				foreach (string item in set) {
					GameObject go = new GameObject ("New Sprite");
					SpriteRenderer renderer = go.AddComponent<SpriteRenderer> ();
					renderer.sprite = ItemManager.Itemlist [item].itemImage;
					go.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 20.0f));
					go.transform.LookAt (Camera.main.transform);
					StartCoroutine (effect (go, item));
				}
			}
		}

		buttonClicked = !buttonClicked;
		Inventory.selectedItems.Clear ();
	}

	public void onSearchButtonClick() {
		if (selectedObject.layer == 8) {
			if (selectedObject.GetComponent<SceneObject> ().canDestroy) {
				selectedObject.GetComponent<SceneObject> ().search ();
				selectedObject = GameObject.Find ("Dummy");
			} else {
				selectedObject.GetComponent<SceneObject> ().search ();
			}
		} else {
			GameManager.invalidUse.Play();
			//use some search time
			GameManager.startTime+= voidSearchTime;
			GameManager.thirst += invalidSearchThirst;
			GameManager.hunger += invalidSearchHunger;
			GameObject.Find ("Hint").GetComponent<Text> ().enabled = true;
			GameObject.Find("Hint").GetComponent<Text>().text = "Nothing was found.";
			StartCoroutine (effect2 ());
		}
		Inventory.selectedItems.Clear ();
		buttonClicked = !buttonClicked;

		GameObject.Find ("Label").GetComponent<Image> ().enabled = false;
	}

	public void onExpandDiaryButtonClick() {
		if (isOpen == false) {
			isOpen = true;
			expandDiaryPanel.gameObject.SetActive (true);
		}
	}

	public void onCloseExpandButtonClick() {
		if (isOpen == true) {
			isOpen = false;
			expandDiaryPanel.gameObject.SetActive (false);
		}
	}

	public void onNextDayButtonClick() {
		Image tmp = GameObject.Find ("night").GetComponent<Image>();

		if (day == GameObject.Find ("EventManager").GetComponent<EventManager> ().RainDay) {
			StartCoroutine (rainDayChange (tmp));
		} else {
			StartCoroutine(lightChange2(tmp, dayTextStr));
		}
	}

	public void onbackToMenuBtnClick() {
		SceneManager.LoadScene (0);
	}

	IEnumerator effect(GameObject go, string item) {
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime) {
			yield return null;
			go.transform.localScale = Vector3.Lerp (go.transform.localScale, new Vector3 (2.0f, 2.0f, 2.0f), i);
		}
		Destroy (go.transform.gameObject);
	}

	IEnumerator effect2() {
		yield return new WaitForSeconds (3);
		GameObject.Find ("Hint").GetComponent<Text> ().enabled = false;
	}

	IEnumerator effect3() {
		yield return new WaitForSeconds (4);
		GameObject.Find ("DiaryText").GetComponent<Text> ().enabled = false;
	}

	IEnumerator effectGameover(GameObject go) {
		for (float i = 0.0f; i < 3.0f; i += Time.deltaTime) {
			yield return null;
			go.transform.localScale = Vector3.Lerp (go.transform.localScale, new Vector3 (2.0f, 2.0f, 2.0f), i);
		}
		go.GetComponent<Image> ().enabled = false;

		SceneManager.LoadScene (0);

		ItemManager.Itemlist.Clear ();
		ItemManager.ItemCombo.Clear ();
		ItemManager.ItemMakeToNotDisappear.Clear ();
	}

	IEnumerator lightChange1 (Image image, string dayText) {

		yield return new WaitForSeconds (1);

		GameObject.Find("dayTextObj").GetComponent<Text>().text = dayText;

		image.enabled = true;
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			image.color = new Color32(0 ,0, 0, k);
		}

		GameObject.Find ("dayTextObj").GetComponent<Text> ().enabled = true;

		for (float i = 2.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			image.color = new Color32(0 ,0, 0, k);
		}

		nextDayButtonGO.gameObject.SetActive (true);
	}

	IEnumerator lightChange2(Image image, string dayText) {
		for (float i = 2.5f; i >= 0.0f; i -= Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.5) * 255);
			image.color = new Color32(0 ,0, 0, k);
		}
		image.enabled = false;

		GameObject.Find ("dayTextObj").GetComponent<Text> ().enabled = false;
		nextDayButtonGO.gameObject.SetActive (false);
	}

	IEnumerator rainDayChange (Image image) {

		GameObject.Find ("dayTextObj").GetComponent<Text> ().enabled = false;

		image.enabled = true;
		GameObject.Find ("RainText1").GetComponent<Text> ().enabled = true;
		GameObject.Find ("RainText2").GetComponent<Text> ().enabled = true;
		GameObject.Find ("RainText3").GetComponent<Text> ().enabled = true;
		GameObject.Find ("RainText4").GetComponent<Text> ().enabled = true;
		nextDayButtonGO.gameObject.SetActive (false);

		rainSound.Play ();
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.5f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.5) * 255);
			GameObject.Find("RainText1").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.5f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.5) * 255);
			GameObject.Find("RainText2").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.5f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.5) * 255);
			GameObject.Find("RainText3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 3.5f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 3.5) * 255);
			GameObject.Find("RainText4").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);

		for (float i = 1.5f; i >= 0.0f; i -= Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 1.5) * 255);
			image.color = new Color32(0 ,0, 0, k);
			GameObject.Find("RainText1").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("RainText2").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("RainText3").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("RainText4").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		image.enabled = false;
		GameObject.Find ("RainText1").GetComponent<Text> ().enabled = false;
		GameObject.Find ("RainText2").GetComponent<Text> ().enabled = false;
		GameObject.Find ("RainText3").GetComponent<Text> ().enabled = false;
		GameObject.Find ("RainText4").GetComponent<Text> ().enabled = false;


	}

	IEnumerator gameStart () {

		GameObject.Find ("Cover1").GetComponent<RawImage> ().enabled = true;
		GameObject.Find ("Cover2").GetComponent<RawImage> ().enabled = true;
		GameObject.Find ("Cover3").GetComponent<RawImage> ().enabled = true;

		GameObject.Find ("CoverText1-1").GetComponent<Text> ().enabled = true;
		GameObject.Find ("CoverText1-2").GetComponent<Text> ().enabled = true;
		GameObject.Find ("CoverText1-3").GetComponent<Text> ().enabled = true;
		GameObject.Find ("CoverText1-4").GetComponent<Text> ().enabled = true;
		GameObject.Find ("CoverText1-5").GetComponent<Text> ().enabled = true;
		GameObject.Find ("CoverText1-6").GetComponent<Text> ().enabled = true;


		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText1-1").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		cover1.Play ();
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText1-2").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText1-3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText1-4").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText1-5").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 3.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 3.0) * 255);
			GameObject.Find("CoverText1-6").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);

		for (float i = 1.5f; i >= 0.0f; i -= Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 1.5) * 255);
			GameObject.Find("Cover1").GetComponent<RawImage>().color = new Color32(255 ,255, 255, k);
			GameObject.Find("CoverText1-1").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("CoverText1-2").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("CoverText1-3").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("CoverText1-4").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("CoverText1-5").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("CoverText1-6").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		GameObject.Find("Cover1").GetComponent<RawImage>().enabled = false;
		GameObject.Find ("CoverText1-1").GetComponent<Text> ().enabled = false;
		GameObject.Find ("CoverText1-2").GetComponent<Text> ().enabled = false;
		GameObject.Find ("CoverText1-3").GetComponent<Text> ().enabled = false;
		GameObject.Find ("CoverText1-4").GetComponent<Text> ().enabled = false;
		GameObject.Find ("CoverText1-5").GetComponent<Text> ().enabled = false;
		GameObject.Find ("CoverText1-6").GetComponent<Text> ().enabled = false;

		rainSound.Play ();
		yield return new WaitForSeconds (3);

		for (float i = 1.5f; i >= 0.0f; i -= Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 1.5) * 255);
			GameObject.Find("Cover2").GetComponent<RawImage>().color = new Color32(255 ,255, 255, k);
		}
		GameObject.Find ("Cover2").GetComponent<RawImage> ().enabled = false;

		cover3.Play ();
		yield return new WaitForSeconds (1);


		GameObject.Find ("CoverText3-1").GetComponent<Text> ().enabled = true;
		GameObject.Find ("CoverText3-2").GetComponent<Text> ().enabled = true;
		GameObject.Find ("CoverText3-3").GetComponent<Text> ().enabled = true;
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText3-1").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText3-2").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("CoverText3-3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);

		for (float i = 1.5f; i >= 0.0f; i -= Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 1.5) * 255);
			GameObject.Find("Cover3").GetComponent<RawImage>().color = new Color32(255 ,255, 255, k);
			GameObject.Find("CoverText3-1").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("CoverText3-2").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("CoverText3-3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		GameObject.Find ("Cover3").GetComponent<RawImage> ().enabled = false;
		GameObject.Find ("CoverText3-1").GetComponent<Text> ().enabled = false;
		GameObject.Find ("CoverText3-2").GetComponent<Text> ().enabled = false;
		GameObject.Find ("CoverText3-3").GetComponent<Text> ().enabled = false;
		background.Play ();
	}


	IEnumerator goodEndingEffect () {

		yield return new WaitForSeconds (1);
		GameObject.Find ("GoodEnding").GetComponent<RawImage> ().enabled = true;

		GameObject.Find ("GoodEndingText1").GetComponent<Text> ().enabled = true;
		GameObject.Find ("GoodEndingText2").GetComponent<Text> ().enabled = true;
		GameObject.Find ("GoodEndingText3").GetComponent<Text> ().enabled = true;


		goodEnding.Play ();
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("GoodEndingText1").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("GoodEndingText2").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 3.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 3.0) * 255);
			GameObject.Find("GoodEndingText3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);

		for (float i = 1.5f; i >= 0.0f; i -= Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 1.5) * 255);
			GameObject.Find("GoodEnding").GetComponent<RawImage>().color = new Color32(255 ,255, 255, k);
			GameObject.Find("GoodEndingText1").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("GoodEndingText2").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("GoodEndingText3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}

		GameObject.Find("GoodEnding").GetComponent<RawImage>().enabled = false;
		GameObject.Find ("GoodEndingText1").GetComponent<Text> ().enabled = false;
		GameObject.Find ("GoodEndingText2").GetComponent<Text> ().enabled = false;
		GameObject.Find ("GoodEndingText3").GetComponent<Text> ().enabled = false;

		SceneManager.LoadScene (0);

		ItemManager.Itemlist.Clear ();
		ItemManager.ItemCombo.Clear ();
		ItemManager.ItemMakeToNotDisappear.Clear ();
	}


	IEnumerator badEndingEffect () {

		yield return new WaitForSeconds (1);

		GameObject.Find ("BadEnding").GetComponent<RawImage> ().enabled = true;

		GameObject.Find ("BadEndingText1").GetComponent<Text> ().enabled = true;
		GameObject.Find ("BadEndingText2").GetComponent<Text> ().enabled = true;
		GameObject.Find ("BadEndingText3").GetComponent<Text> ().enabled = true;

		badEnding.Play ();


		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("BadEndingText1").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 2.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 2.0) * 255);
			GameObject.Find("BadEndingText2").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);
		for (float i = 0.0f; i < 3.0f; i += Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 3.0) * 255);
			GameObject.Find("BadEndingText3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}
		yield return new WaitForSeconds (1);

		for (float i = 1.5f; i >= 0.0f; i -= Time.deltaTime) {
			yield return null;
			byte k = (byte)((i / 1.5) * 255);
			GameObject.Find("BadEnding").GetComponent<RawImage>().color = new Color32(255 ,255, 255, k);
			GameObject.Find("BadEndingText1").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("BadEndingText2").GetComponent<Text>().color = new Color32(255,255,255,k);
			GameObject.Find("BadEndingText3").GetComponent<Text>().color = new Color32(255,255,255,k);
		}

		GameObject.Find("BadEnding").GetComponent<RawImage>().enabled = false;
		GameObject.Find ("BadEndingText1").GetComponent<Text> ().enabled = false;
		GameObject.Find ("BadEndingText2").GetComponent<Text> ().enabled = false;
		GameObject.Find ("BadEndingText3").GetComponent<Text> ().enabled = false;

		SceneManager.LoadScene (0);

		ItemManager.Itemlist.Clear ();
		ItemManager.ItemCombo.Clear ();
		ItemManager.ItemMakeToNotDisappear.Clear ();
	}
}
