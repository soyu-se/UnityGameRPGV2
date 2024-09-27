using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneSkill : MonoBehaviour
{
    public float PullForce { get; set; }
    public float PullDuration { get; set; }
	public float SkillCD { get; set; }
    // Cooldown time after pulling
    private bool isPulling = false;
	private bool isOnCooldown = false;

	[SerializeField] private Transform pullCenter; // Reference to the center of the pull effect


	private void Start()
	{
		// Start the pulling process using Coroutine
		StartCoroutine(PullCycle());
	}

	private IEnumerator PullCycle()
	{
		while (true) // Infinite loop to repeat the cycle
		{
			// Pulling phase
			isPulling = true;
			yield return new WaitForSeconds(PullDuration); // Pull for pullDuration seconds

			// After pulling ends, start the attack cooldown routine
			isPulling = false;
			yield return StartCoroutine(AttackCDRoutine());
			Debug.Log("run form the cyclone script");
		}
	}

	private void Update()
	{
		if (isPulling && PlayerController3.Instance != null && !isOnCooldown)
		{
			// Pull the player towards the center of this object
			Vector3 directionToCenter = (pullCenter.position - PlayerController3.Instance.transform.position).normalized;
			PlayerController3.Instance.GetComponent<Rigidbody2D>().AddForce(directionToCenter * PullForce);
		}
	}

	private IEnumerator AttackCDRoutine()
	{
		// Cooldown phase after pulling
		isOnCooldown = true;
		yield return new WaitForSeconds(SkillCD); // Wait for cooldown duration
		isOnCooldown = false; // Cooldown is over, pulling can resume
	}
}
