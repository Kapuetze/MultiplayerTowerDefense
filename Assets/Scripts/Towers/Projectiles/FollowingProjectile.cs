using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers.Projectiles
{
	public class FollowingProjectile : Projectile
	{
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			if (Target == null)
			{
				Destroy(gameObject);
				return;
			}

			Vector3 dir = Target.transform.position - transform.position;
			float distanceThisFrame = Speed * Time.deltaTime;

			if (dir.magnitude <= distanceThisFrame)
			{
				//deal damage and destroy the projectile
				DealDamage();
				Destroy(gameObject);
				return;
			}

			transform.Translate(dir.normalized * distanceThisFrame, Space.World);
			transform.LookAt(Target.transform);
		}

		void DealDamage()
		{
			//deal damage to the creep
			var creepKillInfo = Target.DealDamage(Damage);

			//if tower has killed the creep, add the gold value to the players gold
			if (creepKillInfo != null)
			{
				Player.CurrentGold += creepKillInfo.Gold;
			}
		}
	}
}