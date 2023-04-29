using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Utilities.EventSystem;

namespace Creeps
{
	public class Creep : NetworkBehaviour
	{
		public int MaxHealth;
		public int GoldValue;
		public float Speed;
		public int Armor;

		[SyncVar(hook = nameof(OnHealthChanged))]
		private int _health;
		private Slider _slider;

		//set init values in awake, because it is called before the server starts
		void Awake()
		{
			_slider = GetComponentInChildren<Slider>();

			//add the creep to a container to keep the GameObject hierarchy clean
			var creepContainer = GameObject.Find("CurrentCreeps");
			transform.parent = creepContainer.transform;
		}

		// Start is called before the first frame update
		public override void OnStartServer()
		{
			_health = MaxHealth;
			gameObject.GetComponent<NavMeshAgent>().speed *= Speed;
		}

        private void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
		{
			
		}

		/// <summary>
		/// Syncvar hook callback function
		/// </summary>
		void OnHealthChanged(int oldHealth, int newHealth)
		{
			// if (_slider != null)
			// {
				_slider.value = (float)newHealth / (float)MaxHealth;
			// }
		}

		/// <summary>
		/// Deal damage to the creep
		/// </summary>
		/// <param name="damage">Damage as int</param>
		/// <returns>True if the creep was destroyed by the damage</returns>
		public CreepKillInfo DealDamage(int damage)
		{
			damage -= Armor;
			_health -= damage;

			if (_health <= 0)
			{
				Die();
				return new CreepKillInfo { Gold = GoldValue };
			}

			return null;
		}

		private void Die()
		{
			GameEvents.current.OnCreepDied();
			Destroy(gameObject);
		}
    }
}