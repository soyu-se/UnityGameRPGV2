using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWindController : MonoBehaviour
{
	[SerializeField] private float health = 100f;

	[Header("Cyclone SKill Attribute")]
	[SerializeField] private float pullForce;
	[SerializeField] private float pullDuration;
	[SerializeField] private float skillCD;
	[SerializeField] private float damage;

	private CycloneSkill cycloneSkill;

	private void Start()
	{
		cycloneSkill = GetComponentInChildren<CycloneSkill>();
		cycloneSkill.PullForce = pullForce;
		cycloneSkill.PullDuration = pullDuration;
		cycloneSkill.SkillCD = skillCD;
	}
}
