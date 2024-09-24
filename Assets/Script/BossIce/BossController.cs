using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	[SerializeField] private SpawnerManager spawnerManager; // Reference to the Swapner Manager
	[SerializeField] private SnowballSpawner snowballSpawner; // Reference to the Snowball Swapper
	[SerializeField] private float bossHealth = 100f; // Boss health


	private void Start()
	{
		//spawnerManager.OnActionComplete += HandleSwapnerManagerComplete; // Subscribe to the event
		StartCoroutine(ManageSpawners());
	}

	private IEnumerator ManageSpawners()
	{
		while (bossHealth > 0)
		{
			// Ensure the Spawner Manager is active
			if (!spawnerManager.gameObject.activeInHierarchy)
			{
				spawnerManager.gameObject.SetActive(true);
			}

			// Run the Spawner Manager
			spawnerManager.Run();

			// Wait until the Spawner Manager completes its action
			yield return new WaitUntil(() => spawnerManager.IsActionComplete);

			// Deactivate the Spawner Manager after it completes its action
			spawnerManager.gameObject.SetActive(false);
			Debug.Log("done for the sword");
			// Ensure the SnowballSpawner is active
			if (!snowballSpawner.gameObject.activeInHierarchy)
			{
				snowballSpawner.gameObject.SetActive(true);
			}

			// Run the Snowball Spawner
			snowballSpawner.Run();

			// Wait until the Snowball Spawner completes its action
			yield return new WaitUntil(() => snowballSpawner.IsShootingComplete); //
			Debug.Log("don for the snow ball");

		}
	}

	private void HandleSwapnerManagerComplete()
	{
		// Run the Snowball Swapper
		snowballSpawner.Run(); // Assuming Run() is a method that starts the snowball spawner
	}

	// Method to reduce boss health (call this when the boss takes damage)
	public void TakeDamage(float damage)
	{
		bossHealth -= damage;
		Debug.Log("boss just take damage");
		if (bossHealth <= 0)
		{
			// Handle boss defeat logic here
			Debug.Log("Boss defeated!");
		}
	}
}
