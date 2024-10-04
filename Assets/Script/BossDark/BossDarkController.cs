using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkController : MonoBehaviour
{
	[SerializeField] private float health = 100f;
	[SerializeField] private SlashSkill slashSkill;
	private void Start()
	{
		StartCoroutine(ManageActivation());
	}

	private IEnumerator ManageActivation()
	{
		yield return new WaitForSeconds(1f);
		while (health > 0)
		{
			yield return new WaitForSeconds(0.5f);

			if (!slashSkill.gameObject.activeInHierarchy)
			{
				slashSkill.gameObject.SetActive(true);
			}

			slashSkill.Run();

			yield return new WaitUntil(() => slashSkill.IsActingComplete);

			slashSkill.gameObject.SetActive(false);

		}
	}
}
