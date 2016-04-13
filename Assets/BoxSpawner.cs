using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour {
	public Transform box;
	public Collider spawnPlane;

	void Start () {
	
	}

	void FixedUpdate () {
		Ray ray = Camera.main.ScreenPointToRay(
			new Vector3(Screen.currentResolution.width / 2, 
						Screen.currentResolution.height / 2, 
						0.0f)
		);
		RaycastHit hit;

		if (spawnPlane.Raycast(ray, out hit, 100.0f))
		{
			if (hit.collider != null)
			{
				box.gameObject.SetActive(true);
				box.position = hit.point;

				var eul = box.rotation.eulerAngles;

				box.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
			}

			else
			{
				box.gameObject.SetActive(false);
			}
		}

		if (SingleButtonInput.GetButtonDown())
		{
			SpawnBox();
		}
	}

	void SpawnBox()
	{
		var state = TowerManager.GetInstance().towerState;
		if (state.Equals(TowerState.STATIONARY))
		{
			Debug.Log("Tower is stationary => spawning box.");
			var spawnedBox = Instantiate(box.gameObject) as GameObject;

			spawnedBox.GetComponent<BoxCollider>().enabled = true;

			// Disable XZ-rotation lock
			spawnedBox.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

			// Ignore collisions with spawn plane
			Physics.IgnoreCollision(spawnPlane, spawnedBox.GetComponent<Collider>());

			// Add it to the tower stack
			TowerManager.GetInstance().addStackedBox(spawnedBox);	
		}

		else if (state.Equals(TowerState.MOVING))
		{
			Debug.Log("Tower is moving => NOT spawning box.");
		}
	}
}
