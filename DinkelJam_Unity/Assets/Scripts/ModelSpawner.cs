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

	[SerializeField] Transform _referenceSpawnLocation;
	[SerializeField] Transform _destructibleSpawnLocation;

	[Header("Model Data")]
	[SerializeField] ModelPair[] _spawningPairs;

	public void SpawnRandomPair() {
		System.Random random = new System.Random();

		int randomIndex = random.Next(0, _spawningPairs.Length);
		ModelPair pair = _spawningPairs[randomIndex];

		GameObject.Instantiate(pair._modelPrefab, _referenceSpawnLocation.position, Quaternion.identity);
		GameObject.Instantiate(pair._destructiblePrefab, _destructibleSpawnLocation.position, Quaternion.identity);
	}
}