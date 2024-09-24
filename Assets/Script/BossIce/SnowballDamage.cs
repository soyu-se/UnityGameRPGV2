using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballDamage : MonoBehaviour
{
	[SerializeField] private int damageAmount = 1;
	[SerializeField] private GameObject iceSwordVFX;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// change the player health later
		if (collision.gameObject.GetComponent<PlayerController3>())
		{
			Debug.Log("player take damage from the boss");
			Instantiate(iceSwordVFX, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
