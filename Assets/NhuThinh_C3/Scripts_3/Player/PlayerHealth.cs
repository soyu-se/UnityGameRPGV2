using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockBack;
    private Flash flash;
    //private Slider healthSlider;

    const string HEALTH_SLIDER_TEXT = "Health Slider"; // Hằng số giống code A

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = maxHealth;

        // Tìm và gán Slider tương ứng cho thanh máu
        //healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy && canTakeDamage)
        {
            TakeDamage(1, collision.transform);  // Thêm Transform để truyền vị trí va chạm
        }
    }

    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)  
    {
        if (!canTakeDamage) return;

        //ScreenShakeManager.Instance.ShakeScreen();
        knockBack.GetKnockedBack(hitTransform, knockBackThrustAmount);

        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();  // death
    }

    private void UpdateHealthSlider()
    {
        // Update Health slider
        //if (healthSlider != null)
        //{
        //    healthSlider.value = (float)currentHealth / maxHealth;
        //}
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
            // Death 
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
