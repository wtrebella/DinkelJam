using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDetector : MonoBehaviour {

	[SerializeField] int _textureSize;

	[Header ("Objects")]
	[SerializeField] Transform _playerObj;
	[SerializeField] Transform _goalObj;

	[Header("Player Test")]
	[SerializeField] Camera _sampleCamera;
	RenderTexture _sampleTexture;
	[SerializeField] Renderer _sampleRender;

	[Header("Reference Test")]
	[SerializeField] Camera _referenceCamera;
	RenderTexture _referenceTexture;
	[SerializeField] Renderer _referenceRender;

	[Header("Test Transforms")]
	[SerializeField] Transform[] _referenceTransforms;
	
	void Awake() {
		_sampleTexture = new RenderTexture(_textureSize, _textureSize, 24, RenderTextureFormat.R8, RenderTextureReadWrite.Default);
		_referenceTexture = new RenderTexture(_textureSize, _textureSize, 24, RenderTextureFormat.R8, RenderTextureReadWrite.Default);

		_sampleCamera.targetTexture = _sampleTexture;
		_sampleCamera.depthTextureMode = DepthTextureMode.Depth;
		_sampleRender.material.mainTexture = _sampleTexture;

		_referenceCamera.targetTexture = _referenceTexture;
		_referenceCamera.depthTextureMode = DepthTextureMode.Depth;
		_referenceRender.material.mainTexture = _referenceTexture;
	}

	void Update () {
		float totalPercentage = 0.0f;
		for(int i = 0; i < _referenceTransforms.Length; i++) {
			totalPercentage += EvaluateTextures(_referenceTransforms[i]);
		}

		//average percentages
		totalPercentage /= _referenceTransforms.Length;
		Debug.Log(totalPercentage);
	}


	float EvaluateTextures(Transform reference) {

		_sampleCamera.transform.position = reference.position;
		_sampleCamera.transform.rotation = reference.rotation;
		_referenceCamera.transform.position = reference.position;
		_referenceCamera.transform.rotation = reference.rotation;

		_playerObj.gameObject.SetActive(true);
		_goalObj.gameObject.SetActive(false);

		_playerObj.position = transform.position;
		_playerObj.rotation = transform.rotation;

		RenderTexture.active = _sampleTexture;
		_sampleCamera.Render();
		Texture2D playerTexture = new Texture2D(_textureSize, _textureSize);
		playerTexture.ReadPixels(new Rect(0, 0, _textureSize, _textureSize), 0, 0);
		playerTexture.Apply();

		_playerObj.gameObject.SetActive(false);
		_goalObj.gameObject.SetActive(true);

		_goalObj.position = transform.position;
		_goalObj.rotation = transform.rotation;

		RenderTexture.active = _referenceTexture;
		_referenceCamera.Render();
		Texture2D goalTexture = new Texture2D(_textureSize, _textureSize);
		goalTexture.ReadPixels(new Rect(0, 0, _textureSize, _textureSize), 0, 0);
		goalTexture.Apply();
		RenderTexture.active = null;

		_sampleRender.material.mainTexture = playerTexture;
		_referenceRender.material.mainTexture = goalTexture;

		float totalPossible = 0;

		int totalMatching = 0;
 
		for(int x = 0; x < _textureSize; x++) {
			for(int y = 0; y < _textureSize; y++) {

				Color playerCol = playerTexture.GetPixel(x, y);
				Color goalCol = goalTexture.GetPixel(x, y);

				if (goalCol != Color.black) {
					totalPossible++;
					if (playerCol != Color.black) {
						totalMatching++;
					}
				}
			}
		}


		float percentMatch = totalMatching / totalPossible;

		return percentMatch;
	}
}