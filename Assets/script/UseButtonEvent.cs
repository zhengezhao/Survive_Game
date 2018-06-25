using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  

public class UseButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Button button;

	public void OnPointerEnter(PointerEventData eventData) {
		button.GetComponent<Image>().color = new Color32 (255, 68, 68, 80);
	}

	public void OnPointerExit(PointerEventData eventData) {
		button.GetComponent<Image>().color = new Color32 (75, 154, 248, 178);
	}
}
