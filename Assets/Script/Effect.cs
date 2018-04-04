using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour {
	public static IEnumerator gainItem(Image im) {
		for (float i = 0.0f; i < 1.0f; i += Time.deltaTime) {
			yield return null;
			im.rectTransform.localScale = Vector3.Lerp (im.rectTransform.localScale, new Vector3(1.0f, 1.0f, 1.0f), i);
			im.rectTransform.localPosition = Vector3.Lerp (im.rectTransform.localPosition, new Vector3(0.0f, 0.0f, 0.0f), i);
		}
	}
}
