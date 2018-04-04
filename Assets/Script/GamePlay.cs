using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamePlay : MonoBehaviour {
	public static int hunger = 80;
	public static int thirst = 80;
	public Text hungerText;
	public Image hungerImage;
	public Text thirstText;
	public Image thirstImage;
	Gradient hungerBarColor = new Gradient();
	Gradient thirstBarColor = new Gradient();

	public static Dictionary<string, SceneManager.SceneItem> sMap;
	public static Dictionary<string, ItemManager.Item> iMap;

	public List<string> list;
	public SceneManager.SceneItem selectedSItem;
	public static string combinedItem;

	public Button useBtn;
	public Button searchBtn;

	// Use this for initialization
	void Start () {
		sMap = initScene ();
		iMap = initItem ();
		hungerBarColor.SetKeys (
			new GradientColorKey[] { new GradientColorKey (Color.red, 0.0f), new GradientColorKey (Color.green, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (1.0f, 0.0f), new GradientAlphaKey (1.0f, 1.0f) });
		thirstBarColor.SetKeys (
			new GradientColorKey[] { new GradientColorKey (Color.red, 0.0f), new GradientColorKey (Color.green, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (1.0f, 0.0f), new GradientAlphaKey (1.0f, 1.0f) });		
	}
	
	// Update is called once per frame
	void Update () {
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () == false) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hitInfo;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
					Debug.Log (hitInfo.transform.name);
					GameObject.Find ("Lable").GetComponent<Renderer> ().enabled = true;
					GameObject.Find ("Lable").transform.position = hitInfo.point;
					searchBtn.GetComponent<Button> ().interactable = true;
					if (sMap.ContainsKey (hitInfo.transform.name)) {
						selectedSItem = sMap [hitInfo.transform.name];
					} else {
						selectedSItem = null;
					}
				} else {
					selectedSItem = null;
				}
			}
		}
		hungerText.text = "Hunger: " + hunger.ToString ();
		hungerImage.rectTransform.localScale = new Vector3 (hunger / 100.0f, 1.0f, 1.0f);
		hungerImage.color = hungerBarColor.Evaluate (hunger / 100.0f);
		thirstText.text = "Thirst: " + thirst.ToString ();
		thirstImage.rectTransform.localScale = new Vector3 (thirst / 100.0f, 1.0f, 1.0f);
		thirstImage.color = thirstBarColor.Evaluate (thirst / 100.0f);
	}


	//BUTTONS
	public void searchButtonFunction() {
		if (selectedSItem != null) { 
			if (selectedSItem.searched == false) {
				selectedSItem.search ();
				StartCoroutine (Effect.gainItem (GamePlay.iMap [selectedSItem.containedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ()));
				selectedSItem = null;
			} else {
				hunger -= 3;
				thirst -= 5;
				selectedSItem = null;
			}
		}
		searchBtn.GetComponent<Button> ().interactable = false;
		GameObject.Find ("Lable").GetComponent<Renderer> ().enabled = false;
	}

	public void itemButtonFunction(Button btn) {
		useBtn.GetComponent<Button> ().interactable = true;
		if (iMap [btn.name].isSelected == false) {
			iMap [btn.name].isSelected = true;
			btn.GetComponent<Outline> ().enabled = true;
		} else {
			iMap [btn.name].isSelected = false;
			btn.GetComponent<Outline> ().enabled = false;
		}
	}

	public void useButtonFunction() {
		foreach (KeyValuePair<string, ItemManager.Item> p in iMap) {
			if (p.Value.isSelected == true) {
				list.Add (p.Key);
			}
		}
		ItemManager.useItem (list);
		if (combinedItem != null) {
			StartCoroutine (Effect.gainItem (GamePlay.iMap [combinedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ()));
			combinedItem = null;
		}
		list.Clear();
		foreach (KeyValuePair<string, ItemManager.Item> p in iMap) {
			if (p.Value.isSelected == true) {
				p.Value.isSelected = false;
				p.Value.go.transform.GetChild (0).transform.GetComponent<Outline> ().enabled = false; 
			}
		}
		useBtn.GetComponent<Button> ().interactable = false;
	}


	//INITIATIONS
	Dictionary<string, SceneManager.SceneItem> initScene(){ 
		Dictionary<string, SceneManager.SceneItem> sceneItemMap = new Dictionary<string, SceneManager.SceneItem> ();

		sceneItemMap.Add("AppleTree", new SceneManager.AppleTree("AppleTree"));
		sceneItemMap.Add("KnifePlace", new SceneManager.KnifePlace("KnifePlace"));
		sceneItemMap.Add("StickPlace", new SceneManager.StickPlace("StickPlace"));

		return sceneItemMap;
	}

	Dictionary<string, ItemManager.Item> initItem(){ 
		Dictionary<string, ItemManager.Item> itemMap = new Dictionary<string, ItemManager.Item> ();

		ItemManager.Item stick = new ItemManager.Item ("Stick");
		itemMap.Add("Stick", stick);

		ItemManager.Item knife = new ItemManager.Item ("Knife");
		itemMap.Add("Knife", knife);

		ItemManager.Item apple = new ItemManager.Item ("Apple");
		itemMap.Add("Apple", apple);

		ItemManager.Item spear = new ItemManager.Item ("Spear");
		itemMap.Add("Spear", spear);

		return itemMap;
	}
}
