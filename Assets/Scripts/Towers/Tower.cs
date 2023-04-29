using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Creeps;
using Players;
using Mirror;
using System;
using Towers.Projectiles;

namespace Towers
{
	public class Tower : NetworkBehaviour
	{
		public TowerData Data;

		[SyncVar]
		[HideInInspector]
		public bool IsBuilt;

		//Variables about the tower
		[HideInInspector]
		public Player Player;
		
		[SyncVar]
		private Guid _towerId;

		//Shooting logic variables
		private Creep _currentTarget;
		private float _timeSinceLastShot;
		private float _timeSinceLastTargetUpdate;
		private Transform _turret;
		private Transform _gun;
		private Animator _animator;
		private AudioSource _gunSound;
		private ParticleSystem _firedBullets;

		private bool _isShooting = false;

		void Awake()
		{
			_turret = transform.Find("tower/Stand_connector");
			_gun = transform.Find("tower/Stand_connector/Joint_bottom/Head/Gun");
			_animator = GetComponentInChildren<Animator>();
			_gunSound = GetComponent<AudioSource>();
			_firedBullets = transform.Find("FiredBullets").GetComponent<ParticleSystem>();
		}

		// Update is called once per frame
		void Update()
		{
			//only shoot if the tower is actually built, not about to be built
			if (IsBuilt)
            {
				FindTarget();
				ShootCurrentTarget();
			}
		}

		private void FindTarget()
		{
			_timeSinceLastTargetUpdate += Time.deltaTime;
			//only check targets every x seconds
			if (_timeSinceLastTargetUpdate > 0.1f )
			{
				_timeSinceLastTargetUpdate = 0;
				//check if we still have a valid target
				if (_currentTarget == null || Vector3.Distance(transform.position, _currentTarget.transform.position) > Data.Range)
				{
					_currentTarget = null;
					//get new (closest) target
					var creeps = GameObject.FindGameObjectsWithTag("Creep");
					float nearestDistance = Mathf.Infinity;
					GameObject nearestCreep = null;
					for (int i = 0; i < creeps.Length; i++)
					{
						var creep = creeps[i];
						float distance = Vector3.Distance(transform.position, creep.transform.position);
						if (distance < nearestDistance)
						{
							nearestDistance = distance;
							nearestCreep = creep;
						}
					}

					if (nearestDistance <= Data.Range)
					{
						_currentTarget = nearestCreep?.GetComponent<Creep>();
					}
				}

				_isShooting = _currentTarget != null;
				if (_isShooting)
				{
					//if we have a target, rotate to it
					_turret.LookAt(_currentTarget.transform);
					_animator.SetBool("IsShooting", true);

					if (!_firedBullets.isPlaying)
						_firedBullets.Play();
					if (!_gunSound.isPlaying)
						_gunSound.Play();
				}
				else
				{
					_animator.SetBool("IsShooting", false);
					_firedBullets.Stop();
					_gunSound.Stop();
				}

			}
		}

		void ShootCurrentTarget()
		{
			_timeSinceLastShot += Time.deltaTime;
			if (_currentTarget != null)
			{
				if (_timeSinceLastShot > Data.AttackSpeed)
				{
					_timeSinceLastShot = 0;

					var projectileGO = Instantiate(Data.Projectile, _gun.position,  Quaternion.identity);
					var projectile = projectileGO.GetComponent<Projectile>();

					//send the projectile to it's target
					projectile.Target = _currentTarget;	
					projectile.Player = Player;					
				}
			}
		}

		[ClientRpc]
		public void RpcSetColor(Color color)
		{
			//Debug.Log("Color changed: " + color);
			GetComponentInChildren<MeshRenderer>().material.color = color;
		}
	}
}