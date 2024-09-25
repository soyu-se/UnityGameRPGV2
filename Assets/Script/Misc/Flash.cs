using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultMaterial = spriteRenderer.material ;
	}


	public float GetRestoreMatTime()
	{
		return restoreDefaultMatTime;
	}

	public IEnumerator FlashRoutine()
	{
		spriteRenderer.material = whiteFlashMaterial;
		yield return new WaitForSeconds(restoreDefaultMatTime);
		spriteRenderer.material = defaultMaterial;
	}
}
