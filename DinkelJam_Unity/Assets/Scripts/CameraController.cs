using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	[SerializeField] float _xSensitivity;
	[SerializeField] float _ySensitivity;
	[SerializeField] Vector2 _yClamp;

	[SerializeField] float _slerpSpeed;

	private float xAngle;
	private float yAngle;

	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		xAngle = 0.0f;
		yAngle = 0.0f;
	}

	void LateUpdate () {
		float mod = _xSensitivity * Time.deltaTime;
		float mouseX = Input.GetAxis("Mouse X") * _xSensitivity * Time.deltaTime;;
		float mouseY = -Input.GetAxis("Mouse Y") * _ySensitivity * Time.deltaTime;;

		xAngle += mouseX;
		yAngle += mouseY;

		yAngle = Mathf.Clamp(yAngle, _yClamp.x, _yClamp.y);

		transform.localRotation = Quaternion.Euler(yAngle, 0f, 0f);
		transform.parent.rotation = Quaternion.Euler(0f, xAngle, 0f);
	}
}