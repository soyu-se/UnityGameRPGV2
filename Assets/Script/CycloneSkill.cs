using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneSkill : MonoBehaviour
{
	[SerializeField] private float pullForce;
	[SerializeField] private float pullDuration;
	//[SerializeField] private float skillCD;
	[SerializeField] private float damage;

	public float skillCD;
	// Cooldown time after pulling
	private bool isPulling = false;
	private bool isOnCooldown = false;
	public bool IsActingComplete { get; private set; }

	[SerializeField] private Transform pullCenter;

	public void Run()
	{
		StartCoroutine(PullCycle());
	}

	private IEnumerator PullCycle()
	{
		IsActingComplete = false;
		// Pulling phase
		isPulling = true;
		yield return new WaitForSeconds(pullDuration);
		// After pulling ends, start the attack cooldown routine
		isPulling = false;
		IsActingComplete = true;
	}

	private void Update()
	{
		if (isPulling && PlayerController3.Instance != null && !isOnCooldown)
		{
			// Pull the player towards the center of this object
			Vector3 directionToCenter = (pullCenter.position - PlayerController3.Instance.transform.position).normalized;
			PlayerController3.Instance.GetComponent<Rigidbody2D>().AddForce(directionToCenter * pullForce);
		}
	}
}
