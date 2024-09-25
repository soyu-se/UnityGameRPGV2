using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int startingHealth = 3;
	[SerializeField] private GameObject deathVFXPrefab;
	[SerializeField] private float knockBackThurst = 15f;

	private int currentHealth;
	private Flash flash;

	public void Awake()
	{
		flash = GetComponent<Flash>();
	}

	private void Start()
	{
		currentHealth = startingHealth;
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		//knockBack.GetknockBack(PlayerController3.Instance.transform, knockBackThurst);
		StartCoroutine(flash.FlashRoutine());
		StartCoroutine(CheckDetectDeathRoutine());
	}

	private IEnumerator CheckDetectDeathRoutine()
	{
		yield return new WaitForSeconds(flash.GetRestoreMatTime());
		DetectDeath();
	}

	public void DetectDeath()
	{
		if (currentHealth <= 0)
		{
			Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
