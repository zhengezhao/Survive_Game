using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Diary : MonoBehaviour {
	static string diary_text;
	static List<string> positiveSen = new List<string> ();
	static List<string> negativeSen = new List<string> ();
	static List<string> neutralSen = new List<string> ();
	static string os_path = "Assets/script/";
	static IEnumerator pos_sen_IEumerator;
	static IEnumerator neg_sen_IEumerator;
	static IEnumerator neu_sen_IEumerator;


	// This method returns an enumerator for positive sentence
	static public IEnumerator SenEnumerator(List<string> Sen)
	{
		// Spit out next string if any.
		foreach (string sentence in Sen) yield return sentence; 	

		// Indicate end of enumeration (optional).
		yield break;
	}



	//public static void Genreating 

	public static void readLines(string filename, List<string> Sens){
		string[] text = System.IO.File.ReadAllLines(os_path+ filename);
		foreach (string t in text) {
			if (t != "") {
				Sens.Add (t);
			}
		}
	}

	//TODO:we don't want to use repeated sentence right now (might be changed later)
	public static void randomizeSen(List<string> Sens){
		for (int i = 0; i < Sens.Count; i++) {
			string temp = Sens[i];
			int randomIndex = Random.Range(i, Sens.Count);
			Sens[i] = Sens[randomIndex];
			Sens[randomIndex] = temp;
		}
		
	}


	public static void addSen(){
		int status = GameManager.LifeStatus ();
		if (status == 1) {
			pos_sen_IEumerator.MoveNext ();
			diary_text += pos_sen_IEumerator.Current;
		}
		if (status == -1) {
			neg_sen_IEumerator.MoveNext ();
			diary_text += neg_sen_IEumerator.Current;
		}
		if (status == 0) {
			neu_sen_IEumerator.MoveNext ();
			diary_text += neu_sen_IEumerator.Current;
		}
	}
		

	//use is either food or drink, 1 for food , 0 for drink
	public static void use_Item(string item_name, int item_property){
		if (item_property == 0) {
			diary_text += (" I drink some"+item_name+".");
			addSen ();

		}

		if (item_property == 1) {
			diary_text += "I got some" + item_name+".";
			addSen ();
		}
		

		//}

	}

	public static void apply_Item(string item_name, int item_property,string objectname){
		if (item_property == -1) {
			diary_text += (" I handle "+objectname+" with "+item_name+".");
			addSen ();	
		}
	}

	public static void make_Item(string item1_name, string item2_name, string madeitem_name){
		diary_text += "I got "+ madeitem_name +"out of " + item1_name + " and " + item2_name+".";
		addSen ();	
		
	}

	// Use this for initialization
	void Start () {
		readLines ("positive.txt", positiveSen);
		readLines ("negative.txt", negativeSen);
		readLines ("neutral.txt", neutralSen);
		randomizeSen (positiveSen);
		randomizeSen (negativeSen);
		randomizeSen (neutralSen);

		pos_sen_IEumerator = SenEnumerator(positiveSen);
		neg_sen_IEumerator = SenEnumerator (negativeSen);
		neu_sen_IEumerator = SenEnumerator (neutralSen);

		//IEnumerator 
//		use_Item("2",2);
//		pos_sen_IEumerator.MoveNext();
//		Debug.Log(pos_sen_IEumerator.Current);
//		use_Item("2",2);

		//while (pos_sen_IEumerator.MoveNext ()) {
		//	Debug.Log (pos_sen_IEumerator.Current);
		//}
		//Debug.Log (getSen (positiveSen));
		//Debug.Log (getSen (positiveSen));

	}


	// Update is called once per frame
	void Update () {
		
	}
}
