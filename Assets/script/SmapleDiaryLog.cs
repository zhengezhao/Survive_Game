using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SmapleDiaryLog : MonoBehaviour {

	public Button button;
	public Text nameLabel;
	string diary_log;


	// Use this for initialization
	void Start () {
		button.onClick.AddListener (delegate{HandleClick(button, Application.persistentDataPath  +"/"+ nameLabel.text+".txt");});
	}

	public void Setup(string filename) {
		string[] filenames = filename.Split ('/');

		nameLabel.text = filenames[filenames.Length-1].Substring(0,filenames[filenames.Length-1].Length-4);
	}

	public void HandleClick(Button btn, string name)
	{

		if (File.Exists (name)) {
			string diary_log= File.ReadAllText(name);
			//Debug.Log (diary_log);
			GameObject.Find ("DiaryText").GetComponent<DiaryText>().text.text = diary_log;

		}
			
	}

}
