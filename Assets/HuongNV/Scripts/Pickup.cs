using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .2f;
    [SerializeField] private float moveSpeed = 3f;
    private Vector3 moveDir;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector3 playerPos = PlayerController3.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelartionRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController3>())
        {
            PlayerHealth.Instance.HealPlayer();
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    // Kiểm tra xem đối tượng va chạm có phải là nhân vật (Player) không
    //    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

    //    if (playerHealth != null)  // Nếu nhân vật có hệ thống sức khỏe
    //    {
    //        playerHealth.Heal(healAmount);  // Hồi máu cho nhân vật
    //        Destroy(gameObject);  // Hủy vật phẩm sau khi nhặt
    //    }
    //}
}
