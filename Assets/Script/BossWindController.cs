using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWindController : MonoBehaviour
{
	[SerializeField] private float health = 100f;
	[SerializeField] private CycloneSkill cycloneSkill;

	private void Start()
	{
		StartCoroutine(ManageActivation());
	}

	private IEnumerator ManageActivation()
	{
		while (health > 0)
		{
			if (!cycloneSkill.gameObject.activeInHierarchy)
			{
				cycloneSkill.gameObject.SetActive(true);
			}

			cycloneSkill.Run();

			yield return new WaitUntil(() => cycloneSkill.IsActingComplete);

			cycloneSkill.gameObject.SetActive(false);

			yield return new WaitForSeconds(cycloneSkill.skillCD);
		}
	}
}
