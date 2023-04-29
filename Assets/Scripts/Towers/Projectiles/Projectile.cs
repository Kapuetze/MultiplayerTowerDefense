using System.Collections;
using System.Collections.Generic;
using Creeps;
using UnityEngine;
using Players;

namespace Towers.Projectiles
{
	public class Projectile : MonoBehaviour
	{
		
		public int Speed;
		public int Damage;

		[HideInInspector]
		public Creep Target;

		/// <summary>
		/// The Player who owns the projectile
		/// </summary>
		[HideInInspector]
		public Player Player;

		// Start is called before the first frame update
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			
		}
	}
}