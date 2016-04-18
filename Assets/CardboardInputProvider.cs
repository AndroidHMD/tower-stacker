using UnityEngine;
using System.Collections;

public class CardboardInputProvider : MonoBehaviour {
	void Start () {
		Cardboard.SDK.TapIsTrigger = true;

		SingleButtonInput.SetGetButtonDelegate(delegate {
			return Cardboard.SDK.Triggered;
		});

		SingleButtonInput.SetGetButtonDownDelegate(delegate {
			return Cardboard.SDK.Triggered;
		});
	}
}
