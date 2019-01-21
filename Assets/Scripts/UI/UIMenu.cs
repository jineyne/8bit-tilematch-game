using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour {
	public GameObject Screen;
	public RectTransform ScreenParent;
	public Vector3 StartOffset = new Vector3(0, -100, 0);
	public Vector3 Offset = new Vector3(0, -500, 0);

	// Use this for initialization
	private void Start () {
		Streamer[] streamers = GameMode.Instance.Streamers;

		for(int i = 0; i < streamers.Length; i++) {
			Vector3 CurrentPosition = StartOffset + (Offset * i);

			GameObject obj = (GameObject)Instantiate(Screen);
			obj.transform.parent = ScreenParent;

			//obj.transform.position = CurrentPosition;
			//obj.transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
