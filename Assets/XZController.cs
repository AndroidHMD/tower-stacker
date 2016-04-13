using UnityEngine;
using System.Collections;

public class XZController : MonoBehaviour {
	public float speed;

	void Update () {
		float right = Input.GetAxis("Horizontal");
		float forward = Input.GetAxis("Vertical");

		var direction = transform.forward * forward + transform.right * right;
		direction.y = 0;
		direction.Normalize();

		transform.position += direction * speed * Time.deltaTime;
	}
}