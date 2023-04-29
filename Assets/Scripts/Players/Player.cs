using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers;
using Mirror;

namespace Players
{
	public class Player : NetworkBehaviour
	{
		[SyncVar(hook = nameof(OnGoldChanged))]
		public int CurrentGold;

		public List<TowerData> AvailableTowers;

		UIManager _uiManager;

		void Awake()
		{
			_uiManager = GameObject.Find("UICanvas").GetComponent<UIManager>();
		}

		public override void OnStartServer()
		{
			CurrentGold = 100;
		}

		/// <summary>
		/// Syncvar hook callback function
		/// </summary>
		void OnGoldChanged(int oldValue, int newValue)
		{
			if (isLocalPlayer)
			{
				_uiManager.SetGold(CurrentGold);
				_uiManager.UpdateAvailableTowers(CurrentGold, AvailableTowers);
			}
		}
	}
}
