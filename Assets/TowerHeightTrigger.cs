using UnityEngine;
using System.Collections;

public class TowerHeightTrigger : MonoBehaviour {
	public float distanceToUpdateTower;
	private Vector3 dvec = new Vector3(0.5f, 0.0f, 0.5f);

	void Update () {
		var tm = TowerManager.GetInstance();
		if (!tm.towerState.Equals(TowerState.STATIONARY))
		{
			return;
		}

		for (int dx = -4; dx <= 4; ++dx)
		{
			for (int dz = -4; dz <= 4; ++dz)
			{
				if (doRayCast(transform.position + new Vector3(dx * dvec.x, 0, dz * dvec.z))) {
					tm.onTowerIsTooHigh();
				}
			}
		}
	}

	bool doRayCast(Vector3 origin)
	{
		var hit = new RaycastHit();

		if (Physics.Raycast(new Ray(origin, new Vector3(0, -1, 0)), out hit))
		{
			if (hit.distance < distanceToUpdateTower)
			{
				return true;
			}
		}

		return false;
	}
}
