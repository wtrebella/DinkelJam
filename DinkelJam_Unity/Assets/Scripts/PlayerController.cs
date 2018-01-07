using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float _moveSpeed;

	bool _previouslyShooting = false;
	float _shootingTime = 0.0f;

	[SerializeField] Transform _cameraTransform;
	[SerializeField] float _fromCenterForce = 300.0f;
	[SerializeField] float _fromCenterRadius = 0.1f;
	[SerializeField] float _fireInterval = 1.0f;
	float _fireTimer = 0.0f;

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

		FireWeapon();
	}

	void FireWeapon() {

		bool holdingDownFire = Input.GetKey (KeyCode.Mouse0);
		if (holdingDownFire) {
			_shootingTime += Time.deltaTime;

			_fireTimer += Time.deltaTime;
			if (_fireTimer > _fireInterval) {
				_fireTimer = 0.0f;

				RaycastHit hitInfo;

				FracturedChunk chunkRaycast = FracturedChunk.ChunkRaycast(_cameraTransform.position, _cameraTransform.forward, out hitInfo);
				if (chunkRaycast) {
					chunkRaycast.FromCenterImpact(hitInfo.point, _fromCenterForce, _fromCenterRadius, true);
				}
				GameAudio.PlayOneShot("GunShot");
			}
		}

		if (!holdingDownFire && _previouslyShooting) {
			//if the previous shot was true and current is false
			//the player just stopped shooting
			GameAudio.PlayOneShot("ShellDrop", new Dictionary<string, float>() {
				{"Shots", _shootingTime}
			});
			_shootingTime = 0.0f;
		}
			
		_previouslyShooting = holdingDownFire;
	}
}
