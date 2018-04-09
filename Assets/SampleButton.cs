using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour {

	public Button button;
	public Text nameLabel;
	public Text amount;
	public Image icon;
	public bool isSelected =false;
	private Item item;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener (delegate{HandleClick(button, nameLabel.text);});
	}

	public void Setup(Item currentItem) {
		item = currentItem;
		nameLabel.text = item.item_name;
		icon.sprite = item.itemImage;
		if (item.property==-1 || item.property==-2) {
			amount.text = "";
		} else {
			amount.text = Inventory.inventory [item.item_name].ToString ();
		}
	}

	public void HandleClick(Button btn, string name)
	{
		Debug.Log (isSelected);
		//GameManager.useBtn.GetComponent<Button>().interactable = true;
		if (isSelected == false) {
			isSelected = true;
			btn.GetComponent<Outline>().enabled = true;
			btn.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
			Inventory.selectedItems.Add (name);
		} else {
			isSelected = false;
			btn.GetComponent<Outline>().enabled = false; 
			btn.GetComponent<Image>().color = Color.white;
			Inventory.selectedItems.Remove(name);
		}
	}
}
