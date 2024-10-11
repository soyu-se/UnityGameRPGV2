using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossDarkController : MonoBehaviour
{
	[SerializeField] private SlashSkill slashSkill;
	[SerializeField] private PlasmaBurstSkill plasmaBurstSkill;
	[SerializeField] private ElectricBallSpawner snowballSpawner;
	[SerializeField] private float GapBetweenSkill = 2f;

	private void Start()
	{
		StartCoroutine(ManageActivation());
	}

	private IEnumerator ManageActivation()
	{
		yield return new WaitForSeconds(1f);

		while (true)
		{

			if (!slashSkill.gameObject.activeInHierarchy)
			{
				slashSkill.gameObject.SetActive(true);
			}

			slashSkill.Run();

			yield return new WaitUntil(() => slashSkill.IsActingComplete);

			slashSkill.gameObject.SetActive(false);

			yield return new WaitForSeconds(GapBetweenSkill);

			if (!plasmaBurstSkill.gameObject.activeInHierarchy)
			{
				plasmaBurstSkill.gameObject.SetActive(false);
			}

			plasmaBurstSkill.Run();
			yield return new WaitUntil(() => plasmaBurstSkill.IsActingComplete);

			plasmaBurstSkill.gameObject.SetActive(false);

			yield return new WaitForSeconds(GapBetweenSkill);

			if (!snowballSpawner.gameObject.activeInHierarchy)
			{
				snowballSpawner.gameObject.SetActive(true);
			}
			snowballSpawner.Run();
			yield return new WaitUntil(() => snowballSpawner.IsShootingComplete);
			snowballSpawner.gameObject.SetActive(false);

			yield return new WaitForSeconds(GapBetweenSkill);
		}
	}
}
