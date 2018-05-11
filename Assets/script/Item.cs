using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

public class Item : MonoBehaviour {

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

	//constructor
	public Item(string newName, float newItem_hunger, float newItem_thirst, int newProperty) {
		item_name = newName;
		item_hunger = newItem_hunger;
		item_thirst = newItem_thirst;
		property = newProperty;
	}
}
