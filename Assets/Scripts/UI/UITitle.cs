using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitle : MonoBehaviour {

	public GameObject BlinkTarget;
	float BlinkInterval = 1f;

	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {
			GameMode.Instance.LoadMenu();
		}

		if((BlinkInterval -= Time.deltaTime) <= 0) {
			BlinkTarget.SetActive(!BlinkTarget.activeSelf);
			BlinkInterval = 1f;
		}
	}
}
