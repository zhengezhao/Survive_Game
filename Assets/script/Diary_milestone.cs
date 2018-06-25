using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using System;

public class Diary_milestone : MonoBehaviour {
	public static string diary_text;
	public static int day_diary;
	public static int today_milestone;
	public int rain_day;


	public static List<string> positiveSen = new List<string> ();
	public static List<string> negativeSen = new List<string> ();
	static List<string> neutralSen = new List<string> ();
	//public static string os_path = "Assets/script/";
	public static IEnumerator pos_sen_IEumerator;
	public static IEnumerator neg_sen_IEumerator;
	public static IEnumerator neu_sen_IEumerator;
	public bool apple_found=false;
	public bool diary_found=false;
	public bool fruitcan_found = false;
	public bool bonfire_found = false;
	public bool meat_found = false;
	public bool thighbone_found = false;
	public bool shell_water_found = false;
	public bool axe_found = false;
	public bool rain_day_found = false;




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
		//string[] text = System.IO.File.ReadAllLines(os_path+ filename);
		var text_all = Resources.Load(filename) as TextAsset;

		string[] text = text_all.text.Split('\n');
		foreach (string t in text) {
			if (t != "") {
				//Debug.Log ("read success");
				//Debug.Log (t);
				Sens.Add (t);
			}
		}
	}

	public static void randomizeSen(List<string> Sens){
		for (int i = 0; i < Sens.Count; i++) {
			string temp = Sens[i];
			int randomIndex = Random.Range(i, Sens.Count);
			Sens[i] = Sens[randomIndex];
			Sens[randomIndex] = temp;
		}

	}

	public static void storeDiary(){
		string filename = System.DateTime.UtcNow.ToString ("HH-mm-MMMM-dd-yyyy")+".txt";
		string full_filename = Path.Combine( Application.persistentDataPath,filename);
		if (!File.Exists (full_filename)) {
			File.WriteAllText((full_filename) , diary_text);
		}

	}
//	public static void addSen(){
//		if (status == 1) {
//			pos_sen_IEumerator.MoveNext ();
//			diary_text += pos_sen_IEumerator.Current;
//		}
//		if (status == -1) {
//			neg_sen_IEumerator.MoveNext ();
//			diary_text += neg_sen_IEumerator.Current;
//		}
//		if (status == 0) {
//			neu_sen_IEumerator.MoveNext ();
//			diary_text += neu_sen_IEumerator.Current;
//		}
//	}






	// Use this for initialization
	void Start () {
		//Debug.Log ("here");
		//Debug.Log (Path.Combine(Application.persistentDataPath,"a.txt"));
		readLines ("positive", positiveSen);
		readLines ("negative", negativeSen);
		readLines ("neutral", neutralSen);
		randomizeSen (positiveSen);
		randomizeSen (negativeSen);
		randomizeSen (neutralSen);

		pos_sen_IEumerator = SenEnumerator(positiveSen);
		neg_sen_IEumerator = SenEnumerator (negativeSen);
		neu_sen_IEumerator = SenEnumerator (neutralSen);


		today_milestone = 0;
		day_diary = 1;
		rain_day = GameObject.Find ("EventManager").GetComponent<EventManager> ().RainDay;
//		diary_text += "\n  Where am I? I don't know.It seems that I am still alive. God bless me. " +
//			"I can survive from such a devastating shipwreck! Where am I? I don't know. " +
//			"Hopefully it is not the helll. It seems to be a deserted island. I need to " +
//			"get some food and drink first. And I still a bonfire to warm my frozen body.";
//		diary_text += "\n \n[Day " + GameManager.day.ToString()+"]\n";
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Inventory.inventory.ContainsKey ("Apple") && !apple_found) {
			today_milestone += 1;
			//float rand = Random.Range (0.0f, 1.0f);
			if (GameManager.day == 1) {
				diary_text += "\nI am a fucking genius! I immediately made a spear using a shoe lace, a stick and a knife. " +
				"This spear looks great and I can reach the apple tree. I will definitely survive with my creativity and wisdom";
			} else if (GameManager.day == 2) {
				diary_text += "\nOn the second day, I made a sharp spear. Althrough it is weried that I can find shoes and knife on the " +
				"island, I don't care. I got apples! Precious food.";				
			} else {
				diary_text += "\nFinally, I got the apples from the stupid apple tree. Is too high for me to reach. " +
				"But I end up making a spear. With that, I got some food. With that, I can barely survive.";
				
			}
			apple_found = true;
		}

		if (Inventory.inventory.ContainsKey ("Bonfire") && !bonfire_found) {
			today_milestone += 1;
			//float rand = Random.Range (0.0f, 1.0f);
			if (GameManager.day == 1) {
				diary_text += "\nBonfire!! Jesus Chirst! Eulogize the sunny weather. I can cook now. First day, it is a big progress. " +
					"This must God's enlightment!" +"God doesn't leave me alone."+"This bonfire is like my life. burning forever!";
			} else if (GameManager.day == 2) {
				diary_text += "\nI start a bonfire, which is so important to my living ." +
					"It is not easy but I still think I can make it."+
					"I can dry my cloth and make my next plan now.";				
			} else {
				diary_text += "\nThis bonefire really takes me years! I am so hungry and totaly frozen!" +
					" I should have drill wood to make fire at the first beginning! How stupid I am!";

			}
			bonfire_found = true;
		}

		if (Inventory.inventory.ContainsKey ("DiaryBook") && !diary_found) {
			today_milestone += 1;
			//float rand = Random.Range (0.0f, 1.0f);
			if (GameManager.day == 1) {
				diary_text += "\nI found a diary book in the bush, it seems that someone has lived in this island. Maybe we can help each other. It is " +
					"great. Such a good news on this bad day.";
			} else if (GameManager.day == 2) {
				diary_text += "\nI accidently got a diary book from the bush. Em.. This is a strong evidence that someone has lived " +
					"here before. Maybe he is still alive.";				
			} else {
				diary_text += "\nI searched the bush today and got a diary book. Someone else is here. Why he didn't show up. Is he watching me thi" +
					"these days? I have to be careful, he might attack me and kill me.";

			}
			diary_found = true;

		}

		if (Inventory.inventory.ContainsKey ("Meat") && !meat_found) {
			today_milestone += 1;
			//float rand = Random.Range (0.0f, 1.0f);
			float rand = Random.Range (0.0f, 1.0f);
			if (rand <1.0f/3.0f) {
				diary_text += "\nMeat!! Meat!! Fresh Meat!! I deserve this feast! This rabbit meat is so tender and juicy! ";
			} else if (rand<2.0f/3.0f) {
				diary_text += "\nThis toasted rabbit meat saves my life. Thanks God for this first feast! I love it." +
				" I can feel the strength coming out of my body again.";		
			} else {
				diary_text += "\nI toast some rabbit meat using the bonfire. Finally I don't need to live like a savage anymore. This meat " +
					"tastes great!";

			}
			meat_found = true;

		}


		if (Inventory.inventory.ContainsKey ("FruitCan") && !fruitcan_found) {
			today_milestone += 1;
			float rand = Random.Range (0.0f, 1.0f);
			if (rand <1.0f/3.0f) {
				diary_text += "\nLuckily I found some fruit can in the shipwreck. This is the only thing I found. But it is not bad. " +
				"Maybe I should go back later.";
			} else if (rand<2.0f/3.0f) {
				diary_text += "\nI searched the shipwreck, everything is ruined except for some fruit can. I guess this will extend my " +
					"life a little. I need to get more food and drinks.";				
			} else {
				diary_text += "\nMy ship is totally broken and soaked in the sea. But I got some fruit can in my storage box.";

			}
			fruitcan_found = true;

		}

		if (GameManager.day == rain_day && !rain_day_found) {
			if (meat_found && bonfire_found) {
				diary_text += "\nMy godness, such a heavy rain! I was totally wet! Jesus! My bonfire totally died!" +
				"Fortunately I have toast my rare meat.";
			} else if (!meat_found && bonfire_found) {
				diary_text += "\nMy godness, such a heavy rain! I was totally wet! God! My bonfire totally died!" +
				"How stupid I am! I should have predicted this rain and uses it to toast some meat. Now, it is just not " +
				"possible anymore. I have to think other ways to survive.";
			} else {
				diary_text += "\nMy godness, such a heavy rain! I got totally wet! Now it is impossible for me to start a bonfire.";
			}

			rain_day_found = true;
		}


		if (Inventory.inventory.ContainsKey ("ThighBone") && !thighbone_found) {
			today_milestone += 1;
			float rand = Random.Range (0.0f, 1.0f);
			if (rand <1.0f/3.0f) {
				diary_text += "\nAfter the rain, some weried stuff emerges out of the sand. I found a human skeleton and get a thighbone" +
					"out of it. I have thought there might be someone on the island. But it seems that he has died long time ago.";
			} else if (rand<2.0f/3.0f) {
				diary_text += "\nI found a human skeleton after the rain, my hope that I can find any alive peroson is totally " +
					"destroyed.";				
			} else {
				diary_text += "\nAfter the rain, I searched the beach and accidently found a human skeleton. He has died long time ago. It s" +
					"seems that he can't make it. But I have to trust myself and get out of this hell!";

			}
			thighbone_found = true;

		}

		if (Inventory.inventory.ContainsKey ("Shell_Water") && !shell_water_found) {
			today_milestone += 1;
			float rand = Random.Range (0.0f, 1.0f);
			if (rand <1.0f/3.0f) {
				diary_text += "\nThe rain is worst but one lucky thing about this is that I store some water in the coconut shell.";
			} else if (rand<2.0f/3.0f) {
				diary_text += "\n.The miracle happens! I was thirsty and when I woke up, I found my coconut shell is full of water. Thanks " +
					"God!";				
			} else {
				diary_text += "\nIt rains last night, and the useless coconut shells are full of precious water! And I can take it with me when" +
					"I am leaving the island.";

			}
			shell_water_found = true;

		}

		if (Inventory.inventory.ContainsKey ("Axe") && !axe_found) {
			today_milestone += 1;
			float rand = Random.Range (0.0f, 1.0f);
			if (rand <1.0f/3.0f) {
				diary_text += "\nI sharpened the blad with a flat stone and made an axe using the thighbone, rope and the blade. Now I have " +
					"an axe, I can use it to cut down the trees.";
			} else if (rand<2.0f/3.0f) {
				diary_text += "\n.A lot of things have been done today. I use the stone as a grinder to sharpen the rusty blade. Then I bound " +
					"the blade and bone with ropes. Now I got a keen axe.";				
			} else {
				diary_text += "\nI never thought I will be a timberjack but today I made an axe by myself.";

			}
			axe_found = true;

		}


	
		
	}
}
