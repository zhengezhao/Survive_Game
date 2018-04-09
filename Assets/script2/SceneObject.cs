using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class contain_Item{
	public string item_name;
	public int item_amount;
}

public class SceneObject : MonoBehaviour {


	public string searchTool;
	public List<contain_Item> itemList;
	public bool isSearched = false;
	public string objectName;
	public bool canDestroy;
	public float hungerCost;
	public float thirstCost;

	public void search() {
		if (!isSearched) {
			foreach (contain_Item item in itemList) {
				Inventory.addItem (item.item_name, item.item_amount);	
			}
			isSearched = true;
			
		}
		if (canDestroy == true) {
			Destroy (transform.gameObject);
		}
		GameManager.hunger += hungerCost;
		GameManager.thirst += thirstCost;

		

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
