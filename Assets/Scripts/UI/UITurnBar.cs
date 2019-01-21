using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnBar : MonoBehaviour {

	public int MaxValue = 900;
	public int MinValue = 10;

	private RectTransform rectTransform;

	[SerializeField]
	private Text TimeIntervalText;

	void Start() {
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		float val = Mathf.Max(0.7f, (Mathf.Pow(GameMode.Instance.TurnInterval - 0.5f, 2) + 0.4f) * 1.4f * 2.5f) - 1.2f;
		float xVal = scale( 0, 1, MinValue, MaxValue, val);
		rectTransform.sizeDelta = new Vector2(xVal, rectTransform.sizeDelta.y);

		TimeIntervalText.text = ((int)GameMode.Instance.TimeLimitInterval).ToString();
	}

	public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
	
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
	
		return(NewValue);
	}
}
