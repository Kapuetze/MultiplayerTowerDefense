using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Towers.Projectiles;

[CreateAssetMenu(fileName = "New Tower", menuName = "TowerData")]
public class TowerData : ScriptableObject
{
	public string Name;
	public string Description;
	public int Range;
	public float AttackSpeed;
	public int Damage;
	public int Cost;
	public Projectile Projectile;
}
