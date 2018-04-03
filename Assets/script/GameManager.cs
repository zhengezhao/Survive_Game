using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static float hunger=10;
	public static float thirst=10;
	public static List<Item> Inventory = new List<Item>();


	public static void UseItem(string newItem){
		for (int i = 0; i < Inventory.Count; i++) {
			if (Inventory [i].item_name == newItem) {
				Inventory [i].itemUse ();
				Inventory.RemoveAt (i);
				break;
			}
		}
		Inventory.Sort ();	
	}

	public static void applyItem(string newItem, GameObject go){
		for (int i = 0; i < Inventory.Count; i++) {
			if (Inventory [i].item_name == newItem) {
				Inventory [i].itemApply (go);
				Inventory.RemoveAt (i);
				break;
			}
		}
		Inventory.Sort ();
	}
		
	public static void makeItem(string item1, string item2){
		foreach (Item a in Inventory) {
			foreach(Item b in Inventory)
			{
				if (a.item_name == item1 && b.item_name == item2) {
					Item res = ItemManager.MakeItem (a.item_name, b.item_name);
					Inventory.Add (res);
					Inventory.Remove(a);
					Inventory.Remove(b);
					Diary.make_Item (a.item_name, b.item_name, res.item_name);

					break;
				}
	
			}
			break;

		}
		Inventory.Sort ();
	}



	// Use this for initialization
	void Start () {
	//Add some default items at the begining of the game

		Inventory.Add (ItemManager.getItem ("knife"));
		Inventory.Sort ();	
	}


	
	// Update is called once per frame
	void Update () {		
	}
}
