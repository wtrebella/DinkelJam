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

	public void SpawnRandomPair(Transform refSpawnLoc, Transform playerSpawnLoc, out GameObject refGobj, out GameObject blockGobj) {
		System.Random random = new System.Random();

		int randomIndex = random.Next(0, _spawningPairs.Length);
		ModelPair pair = _spawningPairs[randomIndex];

		refGobj = GameObject.Instantiate(pair._modelPrefab, refSpawnLoc.position, Quaternion.identity);
		blockGobj = GameObject.Instantiate(pair._destructiblePrefab, playerSpawnLoc.position, Quaternion.identity);
		blockGobj.SetActive(true);

		refGobj.layer = LayerMask.NameToLayer("Art");

		blockGobj.layer = LayerMask.NameToLayer("Art");

		Transform childRoot = blockGobj.transform.GetChild(0);
		int childCount = childRoot.childCount;
		for(int i = 0; i < childCount; i++) {
			Transform child = childRoot.GetChild(i);
			child.gameObject.layer = LayerMask.NameToLayer("Art");
		}
	}
}