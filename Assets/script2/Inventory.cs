using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour {

	public static List<string> selectedItems = new List<string>();
	public static Dictionary<string, int> inventory = new Dictionary<string,int>();
	int InventorySize;
	//public Dictionary<string, Item> objectPrefab = new Dictionary<string, GameObject>();
	public SampleObjPool buttonObjectPool;
	public Transform contentPanel;

	// Use this for initialization
	void Start () {
		InventorySize = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("Update start!");
		if (InventorySize != inventory.Sum (v => v.Value)) {

			foreach (Transform child in contentPanel.transform) {
				Destroy (child.gameObject);
			}

			Debug.Log ("size1: " + InventorySize);
			Debug.Log ("size2: " + inventory.Sum (v => v.Value));
			foreach (KeyValuePair<string, int> pair in inventory) {
				string item = pair.Key;
				Debug.Log (item);

				if (pair.Value > 0) {
					Item itemObj = ItemManager.getItem (item);
					GameObject newButton = buttonObjectPool.GetObject();
					newButton.transform.SetParent(contentPanel);

					SampleButton sampleButton = newButton.GetComponent<SampleButton>();
					sampleButton.Setup (itemObj);
				}
			}
			InventorySize = inventory.Sum (v => v.Value);
		}
	}

	public static void UseItem(string newItem){
		if (inventory.ContainsKey (newItem)) {
			if (inventory [newItem] != 0) {
				Item curItem = ItemManager.getItem (newItem);
				if (curItem.property == 1 || curItem.property == 0 || curItem.property == -1) {
					GameManager.hunger += curItem.item_hunger;
					GameManager.thirst += curItem.item_thirst;

					// Diary.use_Item (curItem.item_name, curItem.property);
				} else if (curItem.property == -2) {
					GameManager.hunger += curItem.decomposeCostHunger;
					GameManager.thirst += curItem.decomposeCostThirst;
					makeItem ();
				}

				selectedItems.Clear ();
				if(ItemManager.getItem(newItem).property==0 ||ItemManager.getItem(newItem).property==1 )
				removeItem (newItem);		
			}
			
		}
	}

	public static void applyItem(string newItem, GameObject go){
		if (inventory.ContainsKey (newItem)) {
			if (inventory [newItem] != 0) {
				ItemManager.getItem (newItem).itemApply (go);
				removeItem (newItem);
			}
		}
	}

	public static void makeItem() {
		HashSet<string> items = ItemManager.MakeItem (selectedItems);

			Debug.Log (items);
		

		if (items != null) {
			//deleted all the selected items
			foreach(string item in selectedItems){
				removeItem (item);
			}
			foreach (string item in items) {
				addItem (item,1);
			}


			if (items.Count == 1) {
				foreach (string item in items) {
					GameManager.hunger += ItemManager.getItem (item).makeCostHunger;
					GameManager.thirst += ItemManager.getItem (item).makeCostThirst;
				}	
			}
				
			//add all made items
			selectedItems.Clear();
			
		}
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
		if (inventory.ContainsKey (item)) {
			inventory [item]+=number;
		} else {
			inventory [item] = number;
		}
	}

}
	
