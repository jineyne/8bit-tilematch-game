using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour {
	public UIScreen Screen;

	private void Start() {
		Screen.StreamerIndex = GameMode.Instance.SelectStreamer;
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.A)) {
			GameMode.Instance.GetTurnManager().OnTurnPass();
		}
	}
}
