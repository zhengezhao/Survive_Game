using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	public int RainDay = 3;
	public bool rainExecuted = false;

	//Rain Event
	public void rainEvent() {
		if (GameManager.day == RainDay && !rainExecuted) {
			StartCoroutine (rainEffect ());
			rainExecuted = true;
		}
	}

	IEnumerator rainEffect() {
		yield return new WaitForSeconds (4);
		Inventory.removeItem("Bonfire");
		if (Inventory.inventory.ContainsKey ("CoconutShell")) {
			Inventory.removeItem("CoconutShell");
			Inventory.addItem ("Shell_Water", 4);
		}
		ItemManager.ItemCombo.Remove (ItemManager.makeString (new List<string>{ "Log_Hole", "Stick", "Straw" }));
		GameObject.Find ("Skeleton_S").GetComponent<SpriteRenderer> ().enabled = true;
		GameObject.Find ("Skeleton_S").GetComponent<BoxCollider> ().enabled = true;
		GameObject.Find ("Rope_S").GetComponent<SpriteRenderer> ().enabled = true;
		GameObject.Find ("Rope_S").GetComponent<BoxCollider> ().enabled = true;
	}
}
