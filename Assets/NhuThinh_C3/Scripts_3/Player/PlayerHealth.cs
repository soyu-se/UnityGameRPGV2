using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    [SerializeField] public float knockBackThrustAmount = 10f;
    [SerializeField] public float damageRecoveryTime = 1f;

    private int currHealth;
    private bool canTakeDamage = true;
    private Knockback knockBack;
    private Flash flash;

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<Knockback>();
    }
    private void Start()
    {
        currHealth = maxHealth;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy=collision.gameObject.GetComponent<EnemyAI>();
        if (enemy && canTakeDamage) 
        {
            TakeDamage(1);
            knockBack.GetKnockedBack(collision.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }
    private void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRountine());
    }
    private IEnumerator DamageRecoveryRountine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}