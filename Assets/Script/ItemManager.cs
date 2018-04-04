using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {
	public Effect effectInstance;

	public class Item {
		public bool isSelected = false;
		public GameObject go;
		public string itemName;
		public Item(string name) {
			this.go = GameObject.Find (name);
			this.itemName = name;
		}
	}

	public static void useItem(List<string> list){
		int length = list.Count;

		// Use a single item directly or Disassemble a item
		if (length == 1) {
			if (list.Contains ("Apple")) {
				GamePlay.hunger += 5;
				GamePlay.thirst += 8;
				GamePlay.iMap ["Apple"].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = false;
				GamePlay.iMap ["Apple"].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = false;
			}

			else if (list.Contains ("Spear")) {
				GamePlay.hunger -= 5;
				GamePlay.thirst -= 8;
				GamePlay.iMap ["Stick"].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = true;
				GamePlay.iMap ["Stick"].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = true;
				GamePlay.iMap ["Knife"].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = true;
				GamePlay.iMap ["Knife"].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = true;
				GamePlay.iMap ["Spear"].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = false;
				GamePlay.iMap ["Spear"].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = false;
			}
			  
			else {
				GamePlay.hunger -= 5;
				GamePlay.thirst -= 8;
			}
		}
	
		// Combine two items
		else if (length == 2) {
			if (list.Contains ("Stick") && list.Contains ("Knife")) {
				GamePlay.hunger -= 5;
				GamePlay.thirst -= 8;

				GamePlay.iMap ["Spear"].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = true;
				GamePlay.iMap ["Spear"].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = true;
				GamePlay.combinedItem = "Spear";

				GamePlay.iMap ["Stick"].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = false;
				GamePlay.iMap ["Stick"].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = false;
				GamePlay.iMap ["Knife"].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = false;
				GamePlay.iMap ["Knife"].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = false;
			} 

			else {
				GamePlay.hunger -= 5;
				GamePlay.thirst -= 8;
			}	
		}
			
		// Other length
		else {
			GamePlay.hunger -= 5;
			GamePlay.thirst -= 8;
		}
	}
}
