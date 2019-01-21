using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager> {
	private Dictionary<string, Object> mCache 
		= new Dictionary<string, Object>();

	public void Load(string subFolder) {
		Object[] t0 = Resources.LoadAll(subFolder);
		for(int i = 0; i < t0.Length; i++) {
			Object resource = t0[i];
			string path = 
				Path.Combine(subFolder, resource.name).Replace("\\", "/");
			mCache[path] = resource;
		}
	}

	public T Get<T>(string name) where T : Object {
		if(mCache.ContainsKey(name)) {
			return (T)mCache[name];
		} else {
			return null;
		}
	}

	public void Awake() {
		Load("Sprites/Menu");
		Load("Sprites/InGame");
		Load("Sprites/Yuki");
		Load("Sprites/Jane");
		Load("Anims/Yuki");
		Load("Anims/Jane");
	}
}
