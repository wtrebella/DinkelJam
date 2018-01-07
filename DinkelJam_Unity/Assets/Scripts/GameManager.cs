using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	[SerializeField] ModelSpawner _modelSpawner;
	public ModelSpawner Spawner { get { return _modelSpawner; }}

    void Awake()
    {
		MakePersistent();  
    }

	void Start() {
		GameAudio.PlayOneShot("Music");
	}
}
