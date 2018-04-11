using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

public class Item : MonoBehaviour {


	//Selected or not in the invertory
	//public bool isSelected = false;

	//if the item has been active or not
	// public bool isActive;

	//if used, the plus or deduction to hunder or thirst
	public float item_hunger;
	public float item_thirst;

	//name of the item
	public string item_name;

	//use int -1,0, 1, -2 to indicate if it is a food,drink or tool . 1: food; 0: drink; -1: tool, -2: composed tool
	public int property;

	public Sprite itemImage;

	public int makeCostHunger;
	public int makeCostThirst;
	public int decomposeCostHunger;
	public int decomposeCostThirst;
	public float madeTime;
	public float decomposeTime;

	// one-to-one make
	public bool canMake;


	//constructor
	public Item(string newName, float newItem_hunger, float newItem_thirst, int newProperty) {
		item_name = newName;
		item_hunger = newItem_hunger;
		item_thirst = newItem_thirst;
		property = newProperty;
		//isSelected = false;
		// isActive = false;
	}




	//use a item for drink or tool, TODO: the hunger should come from the hunger of the status 
	public void itemApply(GameObject go) {
		if (property == -1) {
			GameManager.hunger += item_hunger;
			GameManager.thirst += item_thirst;
			Diary.apply_Item (item_name, property, go.name);
			//Diary.action ("Apply", name, property, go.name);
			// isActive = false;
			//isSelected = false;
		}
	}
		
}
