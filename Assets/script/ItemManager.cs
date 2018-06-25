using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {
	public static Dictionary<string,Item> Itemlist = new Dictionary<string,Item>();	// name -> item
	public static Dictionary<string, HashSet<string>> ItemCombo = new Dictionary<string, HashSet<string>>();
	public static Dictionary<string, List<string>> ItemMakeToNotDisappear = new Dictionary<string, List<string>>(); 
	public Item Apple;
	public Item Knife;
	public Item Stick;
	public Item Shoe;
	public Item Shoelace;
	public Item Spear;
	public Item Log;
	public Item Straw;
	public Item Rabbit;
	public Item Log_Hole;
	public Item Bonfire;
	public Item Raw_Meat;
	public Item Meat;
	public Item FruitCan;
	public Item DiaryBook;
	public Item Robe;
	public Item Sails;
	public Item FlatStone;
	public Item RustyBlade;
	public Item Blade;
	public Item ThighBone;
	public Item Rope;
	public Item Axe;
	public Item WoodPieces;
	public Item Raft;
	public Item CoconutShell;
	public Item Shell_Water;




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
			return ItemCombo[key];
		}
		else {
			GameManager.hunger += 2;
			GameManager.thirst += 2;
			GameManager.startTime += 1;
			return null;
		}
	}

	public static string makeString(List<string> list) {
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
		Itemlist.Add (Knife.item_name, Knife);
		Itemlist.Add (Stick.item_name, Stick);
		Itemlist.Add (Shoe.item_name, Shoe);
		Itemlist.Add (Shoelace.item_name, Shoelace);
		Itemlist.Add (Spear.item_name, Spear);
		Itemlist.Add (Log.item_name, Log);
		Itemlist.Add (Straw.item_name, Straw);
		Itemlist.Add (Rabbit.item_name, Rabbit);
		Itemlist.Add (Log_Hole.item_name, Log_Hole);
		Itemlist.Add (Bonfire.item_name, Bonfire);
		Itemlist.Add (Raw_Meat.item_name, Raw_Meat);
		Itemlist.Add (Meat.item_name, Meat);
		Itemlist.Add (FruitCan.item_name, FruitCan);
		Itemlist.Add (DiaryBook.item_name, DiaryBook);
		Itemlist.Add (Robe.item_name, Robe);
		Itemlist.Add (Sails.item_name, Sails);
		Itemlist.Add (FlatStone.item_name, FlatStone);
		Itemlist.Add (RustyBlade.item_name, RustyBlade);
		Itemlist.Add (Blade.item_name, Blade);
		Itemlist.Add (ThighBone.item_name, ThighBone);
		Itemlist.Add (Rope.item_name, Rope);
		Itemlist.Add (Axe.item_name, Axe);
		Itemlist.Add (WoodPieces.item_name, WoodPieces);
		Itemlist.Add (Raft.item_name, Raft);
		Itemlist.Add (CoconutShell.item_name, CoconutShell);
		Itemlist.Add (Shell_Water.item_name, Shell_Water);


		ItemCombo.Add (makeString(new List<string>{Knife.item_name, Stick.item_name, Shoelace.item_name}), Set(new List<string>{Spear.item_name}));
		ItemMakeToNotDisappear.Add (Spear.item_name, new List<string>(){});
		ItemCombo.Add (makeString(new List<string>{Shoe.item_name}), Set(new List<string>{Shoelace.item_name}));
		ItemMakeToNotDisappear.Add (Shoelace.item_name, new List<string>(){});
		ItemCombo.Add (makeString(new List<string>{Spear.item_name}), Set(new List<string>{Knife.item_name, Stick.item_name, Shoelace.item_name}));
		ItemMakeToNotDisappear.Add (Knife.item_name, new List<string>(){});
		ItemMakeToNotDisappear.Add (Stick.item_name, new List<string>(){});
		ItemCombo.Add (makeString(new List<string>{Log.item_name, Knife.item_name}), Set(new List<string>{Log_Hole.item_name}));
		ItemMakeToNotDisappear.Add (Log_Hole.item_name, new List<string>(){Knife.item_name});
		ItemCombo.Add (makeString(new List<string>{Log_Hole.item_name, Stick.item_name, Straw.item_name}), Set(new List<string>{Bonfire.item_name}));
		ItemMakeToNotDisappear.Add (Bonfire.item_name, new List<string>(){Stick.item_name ,Bonfire.item_name});
		ItemCombo.Add (makeString(new List<string>{Rabbit.item_name, Knife.item_name}), Set(new List<string>{Raw_Meat.item_name}));
		ItemMakeToNotDisappear.Add (Raw_Meat.item_name, new List<string>(){Knife.item_name});
		ItemCombo.Add (makeString(new List<string>{Raw_Meat.item_name, Bonfire.item_name}), Set(new List<string>{Meat.item_name}));
		ItemMakeToNotDisappear.Add (Meat.item_name, new List<string>(){Bonfire.item_name});
		ItemCombo.Add (makeString(new List<string>{Knife.item_name, Robe.item_name}), Set(new List<string>{Sails.item_name}));
		ItemMakeToNotDisappear.Add (Sails.item_name, new List<string>(){Knife.item_name});
		ItemCombo.Add (makeString(new List<string>{RustyBlade.item_name, FlatStone.item_name}), Set(new List<string>{Blade.item_name}));
		ItemMakeToNotDisappear.Add (Blade.item_name, new List<string>(){});
		ItemCombo.Add (makeString(new List<string>{Blade.item_name, ThighBone.item_name, Rope.item_name}), Set(new List<string>{Axe.item_name}));
		ItemMakeToNotDisappear.Add (Axe.item_name, new List<string>(){Rope.item_name});
		ItemCombo.Add (makeString(new List<string>{WoodPieces.item_name, Sails.item_name, Rope.item_name}), Set(new List<string>{Raft.item_name}));
		ItemMakeToNotDisappear.Add (Raft.item_name, new List<string>(){});
	}
}
