using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	public string LevelName;
	public GameObject SceneRoot;

	public GameObject LoadLevel(string levelName)
	{
		Application.LoadLevelAdditive(levelName);

		GameObject levelInstance = GameObject.Find("__LevelRoot");
		var levelRootComponent = levelInstance.GetComponent<LevelRoot>();
		levelRootComponent.onLevelLoaded();

		levelInstance.name = levelName;

		return levelInstance;
	}

	void Start () 
	{
		Debug.Log ("Will try to load level (Scene) with name: " + LevelName);
		GameObject level = LoadLevel (LevelName);
		Debug.Log (level);
		level.transform.SetParent(SceneRoot.transform);
	}
}
