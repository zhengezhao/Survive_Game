using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static float hunger = 10;
	public static float thirst = 10;
	public Button useBtn;

	public Text hungerText;
	public Text thirstText;


	// 1:pos 0: neu -1:neg
	public static int LifeStatus(){
		if (hunger > 15 && thirst > 15) {
			return -1;
		} else if (hunger < 5 && thirst < 5) {
			return 1;
		}
		else{
			return 0;
				
		}
	}
		
	// Use this for initialization
	void Start () {
	//Add some default items at the begining of the game

		Inventory.addItem("apple",3);
		Inventory.addItem ("knife",1);
		Inventory.addItem ("stick",1);

		useBtn.onClick.AddListener (onUseButtonClick);
	}


	// Update is called once per frame
	void Update () {		
		hungerText.text = "Hunger: " + hunger.ToString ();
		thirstText.text = "Thirst: " + thirst.ToString ();
	}

	public void onUseButtonClick() {
		int size = Inventory.selectedItems.Count;

		if (size == 1) {
			Inventory.UseItem (Inventory.selectedItems[0]);
		} else if (size > 1) {
			
			Inventory.makeItem ();
		}
	}
}
