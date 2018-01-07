using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;

public class GameAudio : MonoBehaviour {

	public static void PlayOneShot(string eventName, Dictionary<string, float> paramVals = null) {
		FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + eventName);

		if(paramVals != null) {
			
			foreach(KeyValuePair<string, float> pair in paramVals) {
				FMOD.Studio.ParameterInstance paramInstance;
				eventInstance.getParameter(pair.Key, out paramInstance);
				paramInstance.setValue(pair.Value);
			}
		}

		eventInstance.start();
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
