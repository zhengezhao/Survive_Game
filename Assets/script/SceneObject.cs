using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class contain_Item{
	public string item_name;
	public int item_amount;
}

public class SceneObject : MonoBehaviour {

	public string searchTool="";
	public List<contain_Item> itemList;
	public bool isSearched = false;
	public string objectName;
	public bool canDestroy;
	public float hungerCost;
	public float thirstCost;
	public float searchTime;
	//public bool flag = false;


	public bool hasSearchTool(){
		if (searchTool == "") {
			return true;
		} else {
			if (Inventory.inventory.ContainsKey (searchTool) && Inventory.inventory [searchTool] != 0) {
				return true;
			} else {
				return false;
			}
			
		}
	}

	public void search() {
		GameManager.hunger += hungerCost;
		GameManager.thirst += thirstCost;
		GameManager.startTime += searchTime;
		if (!isSearched && hasSearchTool ()) {
			StartCoroutine (effect (itemList));
			isSearched = true;
		} else if (!isSearched && !hasSearchTool ()) {
			GameManager.invalidUse.Play();
			if (this.name == "AppleTree_S") {
				GameObject.Find ("Hint").GetComponent<Text> ().enabled = true;
				GameObject.Find("Hint").GetComponent<Text>().text = "I need a sharp and long thing to reach the apples.";
				StartCoroutine (effect2 ());
			}
			if (this.name == "Wood_S") {
				GameObject.Find ("Hint").GetComponent<Text> ().enabled = true;
				GameObject.Find("Hint").GetComponent<Text>().text = "I need a tool to cut down these woods";
				StartCoroutine (effect2 ());
			}
			if (this.name == "CoconutTree_S") {
				GameObject.Find ("Hint").GetComponent<Text> ().enabled = true;
				GameObject.Find("Hint").GetComponent<Text>().text = "There is no coconut in this season";
				StartCoroutine (effect2 ());
			}
		} else if (isSearched && hasSearchTool ()) {
			GameManager.invalidUse.Play();
			GameObject.Find ("Hint").GetComponent<Text> ().enabled = true;
			GameObject.Find("Hint").GetComponent<Text>().text = "Nothing was found.";
			StartCoroutine (effect2 ());
		}
	}

	IEnumerator effect(List<contain_Item> itemList) {
		foreach (contain_Item item in itemList) {
			GameManager.validUse.Play();
			Inventory.addItem (item.item_name, item.item_amount);	
			GameObject go = new GameObject ("New Sprite");
			SpriteRenderer renderer = go.AddComponent<SpriteRenderer> ();
			renderer.sprite = ItemManager.Itemlist [item.item_name].itemImage;
			go.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 20.0f));
			go.transform.LookAt (Camera.main.transform);
			for (float i = 0.0f; i < 1.0f; i += Time.deltaTime) {
				yield return null;
				go.transform.localScale = Vector3.Lerp (go.transform.localScale, new Vector3 (2.0f, 2.0f, 2.0f), i);
			}
			Destroy (go.transform.gameObject);
		}
		if (canDestroy) {
			Destroy (transform.gameObject);
		}
	}



	IEnumerator effect2() {
		yield return new WaitForSeconds (3);
		GameObject.Find ("Hint").GetComponent<Text> ().enabled = false;
	}
}
