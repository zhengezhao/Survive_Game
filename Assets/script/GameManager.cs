using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static float hunger = 10;
	public static float thirst = 10;

	public Button useBtn;
	public Button searchBtn;

	public Text hungerText;
	public Text thirstText;
	public static float startTime=0.0f;
	public static float totalTime = 16.0f;
	public static float voidSearchTime = 0.5f;

	public GameObject selectedObject;
	public static bool buttonClicked=false;

	Gradient timeBarColor = new Gradient();
	public Image timeBarImage;

	public RaycastHit hitInfoTmp;


	// 1:pos 0: neu -1:neg
	public static int LifeStatus(){
		if (hunger > 15 && thirst > 15) {
			return -1;
		} else if (hunger < 10 && thirst < 10) {
			return 1;
		}
		else{
			return 0;
				
		}
	}
		
	// Use this for initialization
	void Start () {
	//Add some default items at the begining of the game

		//Inventory.addItem("apple",3);
		//Inventory.addItem ("knife",1);
		//Inventory.addItem ("stick",1);
		selectedObject = GameObject.Find("Dummy");
		useBtn.onClick.AddListener (onUseButtonClick);
		searchBtn.onClick.AddListener (onSearchButtonClick);

		timeBarColor.SetKeys (
			new GradientColorKey[] { new GradientColorKey (Color.black, 0.0f), new GradientColorKey (Color.red, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (1.0f, 0.0f), new GradientAlphaKey (1.0f, 1.0f) });
	}


	// Update is called once per frame
//	void Update () {	
//		hungerText.text = "Hunger: " + hunger.ToString ();
//		thirstText.text = "Thirst: " + thirst.ToString ();
//
//		timeBarImage.rectTransform.localScale= new Vector3 (startTime / 10.0f, 1.0f, 1.0f);
//		timeBarImage.color = timeBarColor.Evaluate (startTime / 1.0f);
//
//		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () == false) {
//			if (Input.GetMouseButtonDown (0)) {
//				RaycastHit hitInfo;
//				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
//					Debug.Log (hitInfo.transform.gameObject.name);
//					GameObject.Find ("Label").GetComponent<SpriteRenderer> ().enabled = true;
//					GameObject.Find ("Label").transform.position = hitInfo.point;
//
//					selectedObject = hitInfo.transform.gameObject;
//				}
//			}
//		}
//	}

	void Update () {    
		hungerText.text = "Hunger: " + hunger.ToString ();
		thirstText.text = "Thirst: " + thirst.ToString ();

		timeBarImage.rectTransform.localScale = new Vector3 (startTime / 10.0f, 1.0f, 1.0f);
		timeBarImage.color = timeBarColor.Evaluate (startTime / 1.0f);

		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () == false) {

			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hitInfo;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
					Debug.Log (hitInfo.transform.gameObject.name);
					selectedObject = hitInfo.transform.gameObject;
					GameObject.Find("Label").GetComponent<Image>().enabled = true;
					GameObject.Find("Label").transform.position = Input.mousePosition;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)
			|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
			Debug.Log ("HAHAH!!");
			GameObject.Find ("Label").GetComponent<Image> ().enabled = false;
			selectedObject = GameObject.Find ("Dummy");
		}
	}


	public void onUseButtonClick() {
		int size = Inventory.selectedItems.Count;
		if (size == 1) {
			string tmpName = Inventory.selectedItems [0];
			Inventory.UseItem (tmpName);

			if (ItemManager.getItem (tmpName).canMake) {
				HashSet<string> set = ItemManager.ItemCombo [tmpName];	
				foreach (string str in set) {
					GameObject go = new GameObject ("New Sprite");
					SpriteRenderer renderer = go.AddComponent<SpriteRenderer> ();
					renderer.sprite = ItemManager.Itemlist [str].itemImage;
					go.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 20.0f));

					go.transform.LookAt (Camera.main.transform);

					StartCoroutine (effect (go, tmpName));
				}
			} else {
				GameManager.hunger += ItemManager.getItem (tmpName).item_hunger;
				GameManager.thirst += ItemManager.getItem (tmpName).item_thirst;
				GameManager.startTime += ItemManager.getItem (tmpName).madeTime;
			}

		} else if (size > 1) {
			HashSet<string> set = Inventory.makeItem ();
			if (set.Count == 1) {
				Debug.Log ("Hello!!!!!");
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
	}

	public void onSearchButtonClick() {
		if (selectedObject.layer == 8) {
			Debug.Log (selectedObject.name);
			if (selectedObject.GetComponent<SceneObject> ().canDestroy) {
				selectedObject.GetComponent<SceneObject> ().search ();	
				selectedObject = GameObject.Find ("Dummy");
			} else {
				selectedObject.GetComponent<SceneObject> ().search ();	
			}
		} else {
			//use some search time
			//GameManager.startTime+= voidSearchTime;
		}
		Inventory.selectedItems.Clear ();
		buttonClicked = !buttonClicked;

		GameObject.Find ("Label").GetComponent<Image> ().enabled = false;
	}

	IEnumerator effect(GameObject go, string item) {
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime) {
			yield return null;
			go.transform.localScale = Vector3.Lerp (go.transform.localScale, new Vector3 (5.0f, 5.0f, 5.0f), i);
		}
		Destroy (go.transform.gameObject);
	}
}
