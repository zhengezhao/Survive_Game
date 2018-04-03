using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : IComparable<Item> {


	//Selected or not in the invertory
	public bool isSelected;

	//if the item has been active or not
	public bool isActive;

	//if used, the plus or deduction to hunder or thirst
	public float item_hunger;
	public float item_thirst;

	//name of the item
	public string item_name;

	//use int -1,0,1 to indicate if it is a food,drink or tool : 1: food; 0: drink; -1: tool
	public int property;

	//constructor
	public Item(string newName, float newItem_hunger, float newItem_thirst, int newProperty){
		item_name = newName;
		item_hunger = newItem_hunger;
		item_thirst = newItem_thirst;
		property = newProperty;
		isSelected = false;
		isActive = false;
	}

	//This method is required by the IComparable
	//interface.
	public int CompareTo(Item other){
		if(other == null)
		{
			return 1;
		}
		//Return the difference in power.
		return this.item_name.CompareTo(other.item_name);
	}


	//use a item for drink or tool, TODO: the hunger,thirst should come from the hunger of the status 
	public void itemUse(){
		if (isSelected == true && isActive == true && (property == 1 || property == 0)){
			GameManager.hunger += item_hunger;
			GameManager.thirst += item_thirst;
			//Diary.action ("USE", name, property,null);
			isActive = false;
			isSelected = false;
		}
	}


	//use a item for drink or tool, TODO: the hunger should come from the hunger of the status 
	public void itemApply(GameObject go){
		if (isSelected == true && isActive == true && property==-1) {
			GameManager.hunger += item_hunger;
			GameManager.thirst += item_thirst;
			//Diary.action ("Apply", name, property, go.name);
			isActive = false;
			isSelected = false;
		}
	}
		
}
