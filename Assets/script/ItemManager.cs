using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
	public static Dictionary<string,Item> Itemlist = new Dictionary<string,Item>();	// name -> item
	//public static Dictionary<HashSet<string>, HashSet<string>> ItemCombo = 
	//new Dictionary<HashSet<string>, HashSet<string>>(HashSet<string>.CreateSetComparer());
	public static Dictionary<string, HashSet<string>> ItemCombo = new Dictionary<string, HashSet<string>>();
	public Item Apple;
	public Item Knife;
	public Item Stick;
	public Item Shoe;
	public Item Shoelace;
	public Item Spear;


	private static HashSet<string> Set(List<string> items) {
		HashSet<string> res = new HashSet<string> ();
		for (int i = 0; i < items.Count; i++) {
			res.Add (items [i]);
		}
		return res;

	}
		

	public static HashSet<string> MakeItem(List<string> items) {
		string key = makeString (items);
		if (ItemCombo.ContainsKey(key)){
			Debug.Log ("Hello");
			return ItemCombo[key];
		}
		else {
			foreach (string name in items) {
				GameManager.hunger += ItemManager.getItem (name).item_hunger;
				GameManager.thirst += ItemManager.getItem (name).item_thirst;
				GameManager.startTime += ItemManager.getItem (name).madeTime;
			}
			return null;
		}
	}

	private static string makeString(List<string> list) {
		list.Sort ();
		string res = "";
		foreach (string str in list) {
			res += str;	
		}
		return res;
	}

	public static Item getItem(string newItem) {
		if (Itemlist.ContainsKey (newItem)) {
			return Itemlist [newItem];
		} else {
			return null;
		}
	}

	// Use this for initialization, build the hashmap and item list
	void Start () {
		Itemlist.Add (Apple.item_name, Apple);
		Itemlist.Add (Stick.item_name, Stick);
		Itemlist.Add (Knife.item_name, Knife);
		Itemlist.Add (Shoe.item_name, Shoe);
		Itemlist.Add (Shoelace.item_name, Shoelace);
		Itemlist.Add (Spear.item_name, Spear);
		ItemCombo.Add (makeString(new List<string>{Knife.item_name, Stick.item_name, Shoelace.item_name}), Set(new List<string>{Spear.item_name}));
		ItemCombo.Add (makeString(new List<string>{Spear.item_name}), Set(new List<string>{Knife.item_name, Stick.item_name, Shoelace.item_name}));
		ItemCombo.Add (makeString(new List<string>{Shoe.item_name}), Set(new List<string>{Shoelace.item_name}));
	}
		
}
