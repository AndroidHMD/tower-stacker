using UnityEngine;
using System.Collections;

public class LevelRoot : MonoBehaviour {
	public void onLevelLoaded()
	{
		var toDeletes = GetComponentsInChildren<DeleteOnLoaded>();

		foreach (var toDelete in toDeletes)
		{
			Destroy(toDelete.gameObject);
		}
	}
}
