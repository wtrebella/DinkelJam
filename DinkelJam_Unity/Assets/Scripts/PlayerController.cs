using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float _moveSpeed;

	void Update() {

		Vector3 movement = new Vector3(0f, 0f, 0f);

		if(Input.GetKey(KeyCode.A)) {
			movement.x -= _moveSpeed;
		}

		if(Input.GetKey(KeyCode.D)) {
			movement.x += _moveSpeed;
		}

		if(Input.GetKey(KeyCode.S)) {
			movement.z -= _moveSpeed;
		}

		if(Input.GetKey(KeyCode.W)) {
			movement.z += _moveSpeed;
		}	

		transform.position += ((transform.right * movement.x) + (transform.forward * movement.z)) * Time.deltaTime;
	}
}
