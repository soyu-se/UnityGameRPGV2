using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 3;
    [SerializeField] public float knockBackThrustAmount = 10f;
    [SerializeField] public float damageRecoveryTime = 1f;

    private Slider healthSlider;
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
        UpdateHealthSlider();
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
    public void HealPlayer()
    {
        if (currHealth < maxHealth)
        {
            currHealth += 1;
            UpdateHealthSlider();
        }
    }
    private void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRountine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currHealth <= 0)
        {
            currHealth = 0;
            Debug.Log("Player Dead");
            Destroy(gameObject);
        }
    }
    private IEnumerator DamageRecoveryRountine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currHealth;
    }
}