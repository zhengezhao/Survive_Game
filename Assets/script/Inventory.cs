using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour {

	public static List<string> selectedItems = new List<string>();
	public static Dictionary<string, int> inventory = new Dictionary<string,int>();
	public SampleObjPool buttonObjectPool;
	public Transform contentPanel;
	int InventorySize;
	public static bool foundApple = false;
	public static bool foundMeat = false;



	// Use this for initialization
	void Start () {
		InventorySize = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (InventorySize != inventory.Sum (v => v.Value) || GameManager.buttonClicked) {

			foreach (Transform child in contentPanel.transform) {
				Destroy (child.gameObject);
			}

			foreach (KeyValuePair<string, int> pair in inventory) {
				string item = pair.Key;

				if (pair.Value > 0) {
					Item itemObj = ItemManager.getItem (item);
					GameObject newButton = buttonObjectPool.GetObject();
					newButton.transform.SetParent(contentPanel);

					SampleButton sampleButton = newButton.GetComponent<SampleButton>();
					sampleButton.Setup (itemObj);
				}
			
			}
			InventorySize = inventory.Sum (v => v.Value);
			GameManager.buttonClicked = !GameManager.buttonClicked;
		}
	}
		

	public static void UseItem(string newItem){
		if (inventory.ContainsKey (newItem)) {
			if (inventory [newItem] != 0) {
				Item curItem = ItemManager.getItem (newItem);
				if (curItem.property == 1 || curItem.property == 0) {
					GameManager.validUse.Play();
					GameManager.hunger += curItem.item_hunger;
					GameManager.thirst += curItem.item_thirst;
				} else if (curItem.property == -2 || curItem.property == -1) {
					GameManager.hunger += curItem.decomposeCostHunger;
					GameManager.thirst += curItem.decomposeCostThirst;
					GameManager.startTime += curItem.decomposeTime;
					makeItem ();
				}
					
				if (ItemManager.getItem (newItem).property == 0 || ItemManager.getItem (newItem).property == 1) {
					removeItem (newItem);	
				}
			}
			
		}
		selectedItems.Clear ();
	}

	public static void applyItem(string newItem, GameObject go){
		if (inventory.ContainsKey (newItem)) {
			if (inventory [newItem] != 0) {
				removeItem (newItem);
			}
		}
	}

	public static HashSet<string> makeItem() {
		HashSet<string> items = ItemManager.MakeItem (selectedItems);
		if (items != null) {
			GameManager.validUse.Play();
			foreach (string item in selectedItems) {
				foreach (string obj in items) {
					if (!ItemManager.ItemMakeToNotDisappear [obj].Contains (item)) {
						removeItem (item);
					}
				}
			}

			foreach (string item in items) {
				if (item == "Meat") {
					addItem (item, 4);
				}
				addItem (item, 1);
			}

			if (items.Count == 1) {
				foreach (string item in items) {
					GameManager.hunger += ItemManager.getItem (item).makeCostHunger;
					GameManager.thirst += ItemManager.getItem (item).makeCostThirst;
					GameManager.startTime += ItemManager.getItem (item).madeTime;
				} 
			}
		} else if (selectedItems.Count == 1 && selectedItems[0] == "DiaryBook") {
			GameManager.bookFlop.Play();
		} else {
			GameManager.invalidUse.Play();
		}

		//add all made items
		selectedItems.Clear();
		return items;
	}

	public static void removeItem(string item) {
		if (inventory.ContainsKey (item)) {
			if (inventory [item] > 0) {
				inventory [item]--;
			} else {
				inventory [item] = 0;
			}
		}
	}

	public static void addItem(string item, int number) {
		if (item == "Apple") {
			foundApple = true;
		} else if (item == "Meat") {
			foundMeat = true;
		}

		if (inventory.ContainsKey (item)) {
			inventory [item]+= number;
		} else {
			inventory [item] = number;
		}
	}

	IEnumerator effectNotificaiton() {
		yield return new WaitForSeconds (3);
		GameObject.Find ("Hint").GetComponent<Text> ().enabled = false;
	}
}
	
