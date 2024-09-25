using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currHealth;
    private bool canTakeDamage = true;
    private Knockback knockBack;
    private Flash flash;
    private Slider healthSlider;

    const string HEALTH_SLIDER_TEXT = "Health Slider"; // Hằng số giống code A

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
        EnemyHealth enemy=collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy && canTakeDamage) 
        {
            TakeDamage(1);  // Thêm Transform để truyền vị trí va chạm
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
    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currHealth -= damageAmount;
        Debug.Log("the player health: " + currHealth);
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


}