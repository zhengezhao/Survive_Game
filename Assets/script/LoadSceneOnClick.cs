using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex) {
		SceneManager.LoadScene (sceneIndex, LoadSceneMode.Single);
		//GameObject.Find ("Canvas").SetActive (false);
		//GameObject.Find ("Canvas").SetActive (true);


		ItemManager.Itemlist.Clear ();
		ItemManager.ItemCombo.Clear ();
		ItemManager.ItemMakeToNotDisappear.Clear ();
	}
}
