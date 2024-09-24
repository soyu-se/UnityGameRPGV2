using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
	[Header("Spawner Attributes")]
	public SwordSpawner[] spawners;  // List of all spawners in the scene
	public float delayBetweenSpawners = 2f;  // Time between each spawner firing

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(SpawnerSequence());
	}

	private IEnumerator SpawnerSequence()
	{
		while (true)
		{
			for (int i = 0; i < spawners.Length; i++)
			{
				spawners[i].Fire();  // Tell the spawner to fire
				yield return new WaitForSeconds(delayBetweenSpawners);  // Wait for the specified delay before moving to the next spawner
			}
		}
	}
}
