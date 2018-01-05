using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{	
	[SerializeField] private Animator _animator;
	[SerializeField] private ParticleSystem _particles;
	[SerializeField] private AudioSource _dinkelJamAudio;

	void Start()
	{
		StartCoroutine(ShowTitle());
	}

	IEnumerator ShowTitle()
	{
		yield return new WaitForSeconds(1.0f);

		_animator.SetTrigger("Show");
	}

	public void OnHitGround()
	{
		_particles.Play();
		_dinkelJamAudio.Play();
	}
}
