using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GameModeSaveData {
	public int SelectStreamer;
}

public class GameMode : MonoSingleton<GameMode>, ISaveable {
	public Streamer[] Streamers;
	public ISaveable[] SaveableObjects;
	public GameObject TitleElement;
	public GameObject MenuElement;
	public GameObject ViewElement;
	public GameObject ResultElement;
	public Main CurrentPuzzle;
	public int Score;
	public float TimeLimitInterval = 30f;

	public bool IsInGame = false;
	
	private int Combo = 0;
	private ResourceManager resourceManager;
	private GameObject managerGameObject;
	[SerializeField]
	private GameModeSaveData ImportantData;
	private Twitch.TurnManager turnManager;

	public int SelectStreamer {
		get{
			return ImportantData.SelectStreamer;
		}
	}

	public ResourceManager GetResourceManager() {
		return resourceManager;
	}

	public Twitch.TurnManager GetTurnManager() {
		return turnManager;
	}

	public Streamer GetStreamer(int index) {
		if(index >= Streamers.Length) {
			return null;
		} else {
			return Streamers[index];
		}
	}

    public string SaveData() {
        return JsonUtility.ToJson(ImportantData);
    }

	public void SaveGame() {
		string data = "mather fucking ub soft server. why not make better?";
	}

	public void LoadMenu() {
		TitleElement.SetActive(false);
		MenuElement.SetActive(true);
		ViewElement.SetActive(false);
		ResultElement.SetActive(false);

		GetComponent<AudioSource>().Stop();
	}

	public void LoadView(int select) {
		ImportantData.SelectStreamer = select;
		IsInGame = true;
		GameMode.Instance.Score = 0;

		TitleElement.SetActive(false);
		MenuElement.SetActive(false);
		ViewElement.SetActive(true);
		ResultElement.SetActive(false);

		GetComponent<AudioSource>().Play();
	}

	public void LoadResult() {
		CurrentPuzzle.Clear();
		TitleElement.SetActive(false);
		MenuElement.SetActive(false);
		ViewElement.SetActive(false);
		ResultElement.SetActive(true);

		GetComponent<AudioSource>().Stop();
	}

	public void LoadPuzzle(Puzzle puzzle) {
		if(CurrentPuzzle) {
			CurrentPuzzle.gameObject.SetActive(false);
		}
		
		if(!CurrentPuzzle.gameObject.activeSelf) {
			CurrentPuzzle.gameObject.SetActive(true);
		}
	}

	public void IncreaseCombo(){
		Combo++;
	}

	public void ResetCombo() {
		Combo = 0;
	}

	public int GetCombo() {
		return Combo;
	}

    private void Awake() {
		managerGameObject = new GameObject("Managers");
		DontDestroyOnLoad(managerGameObject);
		managerGameObject.transform.parent = this.transform;

		resourceManager = managerGameObject.AddComponent<ResourceManager>();
		turnManager = new Twitch.TurnManager();
	}
	private float turnInterval = 1f;
	public float TurnInterval {
		get{
			return turnInterval;
		}
	}

	private void Update() {
		if((turnInterval -= Time.deltaTime * 1f) <= 0) {
			GetTurnManager().OnTurnPass();
			turnInterval = 1f;
		}

		if(IsInGame &&  (TimeLimitInterval -= Time.deltaTime) <= 0) {
			// Game Over
			IsInGame = false;
			GameMode.Instance.LoadResult();
		}
	}
}
