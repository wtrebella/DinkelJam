﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;

public class GameAudio : MonoBehaviour {

	public static void PlayOneShot(string eventName) {
		PlayOneShot(eventName, default(Vector3));
	}

	public static void PlayOneShot(string eventName, Vector3 pos) {
		FMODUnity.RuntimeManager.PlayOneShot("event:/" + eventName, pos);
	}

	public void PlayChunkDetachMarble() {
		//Debug.Log("Poof");
		PlayOneShot("DustPoof");
	}

	public void PlayRubble() {
		//Debug.Log("Debris");
		PlayOneShot("StoneDebris");
	}
}
