using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSpawn : MonoBehaviour
{
	[SerializeField] private GameObject shurikenPrefab;  
	[SerializeField] private int projectile = 3;        
	[SerializeField] private float spawnRadius = 10f;     
	[SerializeField] private float duration = 5f;        // Duration after which shurikens are destroyed

	[SerializeField] private int damage = 1;

	private List<GameObject> spawnedShurikens = new List<GameObject>(); 

	public bool isActionComplete { get; private set; } = false;
	[SerializeField] private ShurikenSkill shurikenSkill;

	public void Run()
	{
		shurikenSkill.Damage = damage;
		StartCoroutine(SpawnShurikens());
	}

	private IEnumerator SpawnShurikens()
	{
		if (PlayerController3.Instance != null)
		{
			for (int i = 0; i < projectile; i++)
			{
				Vector3 playerPosition = PlayerController3.Instance.transform.position;

				Vector2 randomDirection = Random.insideUnitCircle.normalized;
				Vector3 randomPosition = playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0) * Random.Range(0, spawnRadius);

				GameObject shuriken = Instantiate(shurikenPrefab, randomPosition, Quaternion.identity);
				spawnedShurikens.Add(shuriken);
			}
		}

		yield return new WaitForSeconds(duration);

		foreach (GameObject shuriken in spawnedShurikens)
		{
			if (shuriken != null)
			{
				Destroy(shuriken);
			}
		}

		spawnedShurikens.Clear();

		isActionComplete = true; 
	}
}
