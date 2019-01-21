using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthBeat : MonoBehaviour {
	void Update () {
		float val = Mathf.Max(0.7f, (Mathf.Pow(GameMode.Instance.TurnInterval - 0.5f, 2) + 0.4f) * 1.4f * 2.5f);
		this.transform.localScale = new Vector3(val, val, 0);
	}
}
