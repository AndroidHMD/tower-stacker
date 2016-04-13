using UnityEngine;
using System.Collections;

public class LoseTrigger : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
		TowerManager.GetInstance().onObjectTriggeredGround(other);
	}
}
