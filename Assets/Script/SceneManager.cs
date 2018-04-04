using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SceneManager : MonoBehaviour {
	public abstract class SceneItem {
		public bool searched = false;
		public GameObject go;
		public abstract string containedItem { get; }
		public abstract void search ();
		public abstract void use ();
		protected SceneItem(string name) {
			this.go = GameObject.Find (name);
		}
	}

	//SUBCLASSES
	public class AppleTree : SceneItem {
		public override string containedItem {
			get {
				return "Apple";
			}
		}

		public override void search(){
			this.searched = true;
			GamePlay.hunger -= 2;
			GamePlay.thirst -= 2;
			GamePlay.iMap [this.containedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = true;
			GamePlay.iMap [this.containedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = true;
		}
		
		public override void use(){
			//this.selected = false;
			//decrease hunger and thirst
			//some item is activited 
			//optional: some appearance change on this object
		}

		public AppleTree(string name) : base(name){}
	}


	public class KnifePlace : SceneItem {
		public override string containedItem {
			get {
				return "Knife";
			}
		}

		public override void search(){
			this.searched = true;
			GamePlay.hunger -= 2;
			GamePlay.thirst -= 2;
			GamePlay.iMap [this.containedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = true;
			GamePlay.iMap [this.containedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = true;
		}

		public override void use(){
			//this.selected = false;
			//decrease hunger and thirst
			//some item is activited 
			//optional: some appearance change on this object
		}

		public KnifePlace(string name) : base(name){}
	}


	public class StickPlace : SceneItem {
		public override string containedItem {
			get {
				return "Stick";
			}
		}

		public override void search(){
			this.searched = true;
			GamePlay.hunger -= 2;
			GamePlay.thirst -= 2;
			GamePlay.iMap [this.containedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().interactable = true;
			GamePlay.iMap [this.containedItem].go.transform.GetChild (0).transform.GetComponent<Button> ().transform.GetChild (1).transform.GetComponent<Image> ().enabled = true;
		}

		public override void use(){
			//this.selected = false;
			//decrease hunger and thirst
			//some item is activited 
			//optional: some appearance change on this object
		}

		public StickPlace(string name) : base(name){}
	}
		
}




