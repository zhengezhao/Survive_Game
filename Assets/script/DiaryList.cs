using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DiaryList : MonoBehaviour {

	public static List<string> diarylist = new List<string> ();
	public SampleObjPool buttonObjectPool;
	public Transform contentPanel;
	//public static string diary_path = "Assets/diary/";

	public Button diaryBackButton;
	//int currentDiary;


		

	// Use this for initialization
	void Start () {
		//currentDiary = 0;
		//SceneManager.LoadScene (2, LoadSceneMode.Single);
		diarylist.Clear ();
		foreach (Transform child in contentPanel.transform) {
			Destroy (child.gameObject);
		}

		var info = new DirectoryInfo(Application.persistentDataPath);
		var fileInfo = info.GetFiles();
		foreach(FileInfo file in fileInfo){
			if (file.ToString ().Substring (file.ToString ().Length - 3) == "txt" && file.ToString().Substring(0,1)!="o") {
				diarylist.Add (file.ToString ());
			}
		}
		//Debug.Log ("running!!!");
			foreach (string diaryname in diarylist){
			//Debug.Log (diaryname);
			GameObject newButton = buttonObjectPool.GetObject ();
			newButton.transform.SetParent (contentPanel);
			SmapleDiaryLog sampleButton = newButton.GetComponent<SmapleDiaryLog> ();
			sampleButton.Setup (diaryname);
		//	Debug.Log ("running done!!!");
		}

		diaryBackButton.onClick.AddListener (onDiaryBackButtonClick);
	}

	void Update(){
		
		//Canvas.ForceUpdateCanvases ();
//		if (currentDiary < 2) {
//			diarylist.Clear ();
//			var info = new DirectoryInfo (Application.persistentDataPath + "/");
//			var fileInfo = info.GetFiles ();
//			foreach (FileInfo file in fileInfo) {
//				if (file.ToString ().Substring (file.ToString ().Length - 3) == "txt") {
//					diarylist.Add (file.ToString ());
//				}
//			}
//			//Debug.Log ("running!!!");
//			foreach (string diaryname in diarylist) {
//				Debug.Log (diaryname);
//				GameObject newButton = buttonObjectPool.GetObject ();
//				newButton.transform.SetParent (contentPanel);
//				SmapleDiaryLog sampleButton = newButton.GetComponent<SmapleDiaryLog> ();
//				sampleButton.Setup (diaryname);
//				//	Debug.Log ("running done!!!");
//			}
//
//			diaryBackButton.onClick.AddListener (onDiaryBackButtonClick);
//			currentDiary += 1;
//		}
		
//		diarylist.Clear ();
//		Debug.Log ("running!!!");
//		foreach (string diaryname in diarylist){
//			Debug.Log (diaryname);
//			GameObject newButton = buttonObjectPool.GetObject ();
//			newButton.transform.SetParent (contentPanel);
//			SmapleDiaryLog sampleButton = newButton.GetComponent<SmapleDiaryLog> ();
//			sampleButton.Setup (diaryname);
//			Debug.Log ("running done!!!");
//		}
//
//		diaryBackButton.onClick.AddListener (onDiaryBackButtonClick);
//		
	}



	public void onDiaryBackButtonClick() {
		SceneManager.LoadScene (0);
	}
}
