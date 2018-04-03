using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
	public static Dictionary<string,Item> Itemlist = new Dictionary<string,Item>();
	//Dictionary<HashSet<string>, Item> ItemCombo = new Dictionary<HashSet<string>, Item>(HashSet<string>.CreateSetComparer());
	public static Dictionary<HashSet<string>, Item> ItemCombo = new Dictionary<HashSet<string>, Item>();
	public Item apple;
	public Item knife;
	public Item stick;
	public Item spike;


	private static HashSet<string> Set(string item1, string item2){
		HashSet<string> res = new HashSet<string> ();
		res.Add (item1);
		res.Add (item2);
		return res;

	}

	public static Item MakeItem(string item1, string item2){
		if (ItemCombo.ContainsKey(Set(item1,item2))){
			return ItemCombo[Set(item1,item2)];
		}
		else{
			return null;
		}
	}

	public static Item getItem(string newItem){
		if (Itemlist.ContainsKey (newItem)) {
			return Itemlist [newItem];
		} else {
			return null;
		}
	}




	// Use this for initialization, build the hashmap and item list
	void Start (){
		Itemlist.Add (apple.item_name,apple);
		Itemlist.Add (stick.item_name,knife);
		Itemlist.Add (knife.item_name,apple);
		Itemlist.Add (spike.item_name,spike);
		ItemCombo.Add (Set(knife.item_name,stick.item_name), spike);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
