using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumLevelManager : Singleton<MuseumLevelManager> 
{
	[SerializeField] Transform _referenceStatueTransform;
	[SerializeField] Transform _playerStatueTransform;

	public Transform ReferenceStatueTransform { get { return _referenceStatueTransform; } }
	public Transform PlayerStatueTransform { get { return _playerStatueTransform; } }

	[SerializeField] ShapeDetector _shapeDetector;

	private GameObject _currentReference;
	private GameObject _currentBlock;

	void Start() {

		GameManager.instance.Spawner.SpawnRandomPair(_referenceStatueTransform, _playerStatueTransform, out _currentReference, out _currentBlock);
		_shapeDetector.SetTransforms(_currentReference.transform, _currentBlock.transform);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			float percent = _shapeDetector.GetPercentage();
			Debug.Log(percent);
		}
	}
}