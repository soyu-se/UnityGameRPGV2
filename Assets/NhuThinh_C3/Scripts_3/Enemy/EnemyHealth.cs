using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int startingHealth = 3;
	[SerializeField] private GameObject deathVFXPrefab;
	[SerializeField] private float knockBackThurst = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Start()
    {
        currentHealth = startingHealth;
    }
    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController3.Instance.transform, knockBackThurst);
        StartCoroutine(flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
