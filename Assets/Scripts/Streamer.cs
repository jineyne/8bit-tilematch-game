using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StreamerSaveData {
	public string Name;
	public Animation TheAnimation;
	public string SpriteUrl;
}

public class Streamer : ScriptableObject, ISaveable {

	public string Name {
		get{
			return ImportantData.Name;
		}
	}

	public Sprite DefaultImage {
		get {
			return mDefaultImage;
		}
	}
	
	[SerializeField]
	private StreamerSaveData ImportantData;

	[SerializeField]
	private Sprite mDefaultImage;

	// Use this for initialization
	private void Awake () {
		// ResourceManager resourceManager = GameMode.Instance.GetResourceManager();
		// mDefaultImage = resourceManager.Get<Sprite>(string.Format("Sprites/{0}/Default", Name));
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    public string SaveData()
    {
        return JsonUtility.ToJson(ImportantData);
    }
}
