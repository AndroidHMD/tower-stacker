using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
	public TowerState towerState 
	{
		get; private set;
	}

	/// <summary>
	///  Singleton stuff
	/// </summary>
	private static TowerManager singleton;

	public static TowerManager GetInstance()
	{
		return singleton;
	}

	/// <summary>
	/// Class stuff
	/// </summary>
	/// 

	private string labelStr = "Nothing to show.";

	public Collider groundTrigger;
	public Collider heightTrigger;

	public float stationaryLinearVelocityBound = 0.1f;
	public float stationaryAngularVelocityBound = 0.1f;

	public GameObject platformParent;

	public float heightIncrement = 4.0f;
	public float heightAnimationTime = 2.0f;

	private List<GameObject> stackedBoxes;

	public void addStackedBox(GameObject box)
	{
		stackedBoxes.Add(box);
		heightTrigger.enabled = false;
	}


	bool stackedBoxesAreStationary()
	{
		foreach(var stackedBox in stackedBoxes)
		{
			var rb = stackedBox.GetComponent<Rigidbody>();

			var linSpeed = rb.velocity.magnitude;
			var angSpeed = rb.angularVelocity.magnitude;

			if (linSpeed > stationaryLinearVelocityBound || angSpeed > stationaryAngularVelocityBound)
			{
				return false;
			}
		}

		return true;
	}

	void Start ()
	{
		singleton = this;

		stackedBoxes = new List<GameObject>();
	}

	void updateTowerState()
	{
		var isStationary = stackedBoxesAreStationary();

		if (isStationary)
		{
			setStationary();
		}

		else
		{
			towerState = TowerState.MOVING;
		}
	}

	void setStationary()
	{
		towerState = TowerState.STATIONARY;
		heightTrigger.enabled = true;
	}

	void startUpdateTowerHeight()
	{
		Vector3 newPlatformPosition = platformParent.transform.position + new Vector3(0.0f, heightIncrement, 0.0f);

		iTween.MoveTo(platformParent, newPlatformPosition, heightAnimationTime);
		Invoke("setStationary", heightAnimationTime);
	}

	void updatePlatformHeight()
	{
		foreach(var stackedBox in stackedBoxes)
		{
			var boxCollider = stackedBox.GetComponent<BoxCollider>();

			if (heightTrigger.bounds.Intersects(boxCollider.bounds))
			{
				log("Tower reached specific height, increasing height!");
				towerState = TowerState.INCREASING_HEIGHT;

				startUpdateTowerHeight();

				return;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		updateTowerState();

		if (!towerState.Equals(TowerState.MOVING))
		{
			updatePlatformHeight();
		}
	}

	public void onObjectTriggeredGround(Collider objCollider)
	{
		var obj = objCollider.gameObject;

		if (stackedBoxes.Count == 0)
		{
			return;
		}

		if (stackedBoxes[0].Equals(obj))
		{
			log("The first box hit the ground.");
		}

		else
		{
			log("A second object hit the ground. You lost!");
		}
	}

	void OnGUI()
	{
		GUILayout.Label(labelStr);
		GUILayout.Label(towerState.ToString());
	}

	void log(string str)
	{
		Debug.Log(str);
		labelStr = str;
	}
}