using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneSkill : MonoBehaviour
{
	[SerializeField] private float pullForce = 10f;
	[SerializeField] private float pullDuration = 5f;
	[SerializeField] private int damage = 1;
	[SerializeField] private Transform pullCenter;
	[SerializeField] private float randomRadius = 3f;

	public float skillCD = 2f;
	// Cooldown time after pulling
	private bool isPulling = false;
	private bool isOnCooldown = false;
	public bool IsActingComplete { get; private set; }

	public void Run()
	{
		StartCoroutine(PullCycle());
		SetRandomPullCenter();
	}

	private void SetRandomPullCenter()
	{
		if (PlayerController3.Instance != null)
		{
			// Get the player's position
			Vector3 playerPosition = PlayerController3.Instance.transform.position;

			// Generate a random position near the player within the specified radius
			Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
			Vector3 randomPosition = playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0) * UnityEngine.Random.Range(0, randomRadius);

			// Set the pullCenter position to the new random position
			pullCenter.position = randomPosition;
		}
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<PlayerController3>())
		{
			PlayerHealth playerHealth = PlayerController3.Instance.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(damage, this.transform);
		}
	}
}
