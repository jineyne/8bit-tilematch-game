using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResult : MonoBehaviour {

	public GameObject Score;
	public GameObject Credit;
	public Text ResultScoreText;
	public Image img;

	public float PassInterval = 10f;

	// Use this for initialization
	void Start () {
		Score.SetActive(true);
		Credit.SetActive(false);

		Debug.Log(GameMode.Instance.Score);
		ResultScoreText.text = GameMode.Instance.Score.ToString();

		StartCoroutine(FadeImage());
	}
	IEnumerator FadeImage()
    {
		while((PassInterval -= Time.deltaTime) > 0) { yield return null;}
		// loop over 1 second
		for (float i = 0; i <= 1; i += Time.deltaTime)
		{
			// set color with i as alpha
			img.color = new Color(1, 1, 1, i);
			yield return null;
		}

		Score.SetActive(false);
		Credit.SetActive(true);

		// loop over 1 second backwards
		for (float i = 1; i >= 0; i -= Time.deltaTime)
		{
			// set color with i as alpha
			img.color = new Color(1, 1, 1, i);
			yield return null;
		}
		PassInterval = 10f;
		while((PassInterval -= Time.deltaTime) > 0) { yield return null;}

		GameMode.Instance.LoadMenu();
		
    }
}
