using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
		if(!isSearched && hasSearchTool() ) {
			foreach (contain_Item item in itemList) {
				Inventory.addItem (item.item_name, item.item_amount);	

				GameObject go = new GameObject("New Sprite");
				SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
				renderer.sprite = ItemManager.Itemlist [item.item_name].itemImage;
				go.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width / 2, Screen.height / 2, 20.0f) );
				go.transform.LookAt(Camera.main.transform);

				StartCoroutine (effect (go, item));
				Diary.search (this.objectName, this.searchTool, this.itemList);

			}
			isSearched = true;
			
		}
//		if (canDestroy == true) {
//			Destroy (transform.gameObject);
//			flag = false;
//		}
		GameManager.hunger += hungerCost;
		GameManager.thirst += thirstCost;
		GameManager.startTime += searchTime;
	}

	IEnumerator effect(GameObject go, contain_Item item) {
		Debug.Log ("EFFECT");
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime) {
//			if (i >= 0.0003f) {
//				break;
//			}
			yield return null;
			go.transform.localScale = Vector3.Lerp (go.transform.localScale, new Vector3 (5.0f, 5.0f, 5.0f), i);
		}

		Destroy (go.transform.gameObject);
		if (canDestroy) {
			Destroy (transform.gameObject);
		}
	}

}
