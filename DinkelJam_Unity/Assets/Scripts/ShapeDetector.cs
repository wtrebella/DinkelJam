using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDetector : MonoBehaviour {

	[SerializeField] int _textureSize;

	[Header ("Objects")]
	Transform _playerObj;
	Transform _goalObj;

	[Header("Player Test")]
	[SerializeField] Camera _sampleCamera;
	RenderTexture _sampleTexture;

	[Header("Reference Test")]
	[SerializeField] Camera _referenceCamera;
	RenderTexture _referenceTexture;

	[Header("Test Transforms")]
	[SerializeField] Transform[] _referenceTransforms;

	[SerializeField] Renderer _referenceRender;
	[SerializeField] Renderer _playerRender;
	
	void Awake() {
		_sampleTexture = new RenderTexture(_textureSize, _textureSize, 24, RenderTextureFormat.R8, RenderTextureReadWrite.Default);
		_referenceTexture = new RenderTexture(_textureSize, _textureSize, 24, RenderTextureFormat.R8, RenderTextureReadWrite.Default);

		_sampleCamera.targetTexture = _sampleTexture;
		_sampleCamera.depthTextureMode = DepthTextureMode.Depth;

		_referenceCamera.targetTexture = _referenceTexture;
		_referenceCamera.depthTextureMode = DepthTextureMode.Depth;
	}

	public void SetTransforms(Transform goal, Transform playerObj) {
		_playerObj = playerObj;
		_goalObj = goal;
	}

	public float GetPercentage() {

		if (_playerObj == null || _goalObj == null) {
			Debug.LogError("Either the player object or the goal object were not properly set before trying to evaluate");
			return 0.0f;
		}

		float totalPercentage = 0.0f;

		for(int i = 0; i < _referenceTransforms.Length; i++) {
			totalPercentage += EvaluateTextures(_referenceTransforms[i]);
		}

		//average percentages
		totalPercentage /= _referenceTransforms.Length;

		return totalPercentage;
	}


	float EvaluateTextures(Transform reference) {

		_playerObj.gameObject.SetActive(true);
		_goalObj.gameObject.SetActive(false);

		transform.position = _playerObj.position;
		_sampleCamera.transform.position = reference.position;
		_sampleCamera.transform.rotation = reference.rotation;

		RenderTexture.active = _sampleTexture;
		_sampleCamera.Render();
		Texture2D playerTexture = new Texture2D(_textureSize, _textureSize);
		playerTexture.ReadPixels(new Rect(0, 0, _textureSize, _textureSize), 0, 0);
		playerTexture.Apply();

		_playerObj.gameObject.SetActive(false);
		_goalObj.gameObject.SetActive(true);

		transform.position = _goalObj.position;
		_referenceCamera.transform.position = reference.position;
		_referenceCamera.transform.rotation = reference.rotation;

		RenderTexture.active = _referenceTexture;
		_referenceCamera.Render();
		Texture2D goalTexture = new Texture2D(_textureSize, _textureSize);
		goalTexture.ReadPixels(new Rect(0, 0, _textureSize, _textureSize), 0, 0);
		goalTexture.Apply();
		RenderTexture.active = null;

		_playerObj.gameObject.SetActive(true);
		_goalObj.gameObject.SetActive(true);

		if (_referenceRender != null) {
			_referenceRender.material.mainTexture = goalTexture;
		}

		if (_playerRender != null) {
			_playerRender.material.mainTexture = playerTexture;
		}

		float totalPossible = 0;

		int totalMatching = 0;
 
		for(int x = 0; x < _textureSize; x++) {
			for(int y = 0; y < _textureSize; y++) {

				Color playerCol = playerTexture.GetPixel(x, y);
				Color goalCol = goalTexture.GetPixel(x, y);

				totalPossible++;

				if((goalCol == Color.black && playerCol == Color.black)
				|| (goalCol != Color.black && playerCol != Color.black)) {
					totalMatching++;
				}
			}
		}


		float percentMatch = totalMatching / totalPossible;

		return percentMatch;
	}
}