using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumLevelManager : Singleton<MuseumLevelManager> 
{
	[SerializeField] Transform _referenceStatueTransform;
	[SerializeField] Transform _playerStatueTransform;

	public Transform ReferenceStatueTransform { get { return _referenceStatueTransform; } }
	public Transform PlayerStatueTransform { get { return _playerStatueTransform; } }

	void Start() {
		GameManager.instance.Spawner.SpawnRandomPair(_referenceStatueTransform, _playerStatueTransform);
	}
}