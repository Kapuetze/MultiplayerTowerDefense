using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Towers;
using Mirror;
using Utilities.EventSystem;

namespace Players
{
	public class Builder : NetworkBehaviour
	{
		private Color _playerColor;
		private GameObject _objectToPlace;
		private Grid _grid;
		private Player _player;

		// Start is called before the first frame update
		void Start()
		{
			_player = GetComponent<Player>();
			var terrain = GameObject.Find("TerrainGroup_0");
			_grid = terrain.GetComponentInChildren<Grid>();

			//Register listeners
			BuildTowerSelectedEvent.RegisterListener(SetObjectToPlace);
		}

		// Update is called once per frame
		void Update()
		{
			if (_objectToPlace != null)
			{
				//show object and snap to grid
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out RaycastHit hit))
				{
					var point = GetNearestGridPoint(hit.point);
					//place object on mouse pos
					_objectToPlace.transform.position = point;

					//actually place object when left clicking
					if (Input.GetMouseButtonDown(0))
					{
						//spawn tower on server
						CmdSpawnTower(point, _objectToPlace.GetComponent<Tower>().Data.name);

						Destroy(_objectToPlace);
						_objectToPlace = null;
					}
				}
			}
		}

		public void SetPlayerColor(Color color)
		{
			_playerColor = color;
		}

		void SetObjectToPlace(BuildTowerSelectedEvent data)
		{
			var objectPrefab = Resources.Load("Towers/" + data.TowerData.name + "/" + data.TowerData.name) as GameObject;
			_objectToPlace = Instantiate(objectPrefab);
		}

		Vector3 GetNearestGridPoint(Vector3 point)
		{
			Vector3Int cellPosition = _grid.WorldToCell(point);
			return _grid.GetCellCenterWorld(cellPosition);
		}

		[Command]
		void CmdSpawnTower(Vector3 point, string towerName)
		{
			var objectPrefab = Resources.Load("Towers/" + towerName + "/" + towerName) as GameObject;
			int towerCost = objectPrefab.GetComponentInChildren<Tower>().Data.Cost;
			
			if (_player.CurrentGold >= towerCost)
			{
				GameObject newTower = Instantiate(objectPrefab, point, Quaternion.identity);
				Tower tower = newTower.GetComponentInChildren<Tower>();
				NetworkServer.Spawn(newTower);
				tower.IsBuilt = true;
				tower.Player = _player;
				tower.RpcSetColor(_playerColor);

				_player.CurrentGold -= towerCost;
			}
		}
	}
}
