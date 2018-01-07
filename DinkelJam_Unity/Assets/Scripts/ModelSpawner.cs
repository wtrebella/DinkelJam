using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ModelPair {
	public string _name;
	public GameObject _modelPrefab;
	public GameObject _destructiblePrefab;
}

public class ModelSpawner : MonoBehaviour {

	[Header("Model Data")]
	[SerializeField] ModelPair[] _spawningPairs;

	public void SpawnRandomPair(Transform refSpawnLoc, Transform playerSpawnLoc) {
		System.Random random = new System.Random();

		int randomIndex = random.Next(0, _spawningPairs.Length);
		ModelPair pair = _spawningPairs[randomIndex];

		GameObject.Instantiate(pair._modelPrefab, refSpawnLoc.position, Quaternion.identity);
		GameObject spawnedBlock = GameObject.Instantiate(pair._destructiblePrefab, playerSpawnLoc.position, Quaternion.identity);
		spawnedBlock.SetActive(true);
	}
}