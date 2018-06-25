using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasrefresh : MonoBehaviour {

	public GameObject canvas_ob;

	// Use this for initialization
	void Start () {

		canvas_ob.SetActive (false);
		canvas_ob.SetActive (true);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
