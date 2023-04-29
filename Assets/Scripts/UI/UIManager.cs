using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Towers;
using UnityEngine;
using UnityEngine.UI;
using Utilities.EventSystem;

public class UIManager : MonoBehaviour
{
	public GameObject TowerButtonPrefab; 

	Text _creepCount;
	Text _goldCount;
	Transform _towerContainer;

	List<GameObject> _towerButtons = new List<GameObject>();

	int _count = 0;

    // Start is called before the first frame update
    void Start()
    {
		//reference the text fields
		_creepCount = transform.Find("CreepCountLabel/CreepCount").GetComponent<Text>();
		_goldCount = transform.Find("GoldCountLabel/GoldCount").GetComponent<Text>();

		//get the container for towers
		_towerContainer = transform.Find("TowerContainer");

		//Register listeners
		GameEvents.current.onCreepSpawnTrigger += OnCreepSpawn;
		GameEvents.current.onCreepDiedTrigger += OnCreepDied;
	}
	private void OnCreepSpawn()
	{
		_count++;
		_creepCount.text = _count.ToString();
	}

	private void OnCreepDied()
	{
		_count--;
		_creepCount.text = _count.ToString();
	}

	public void SetGold(int gold)
	{
		_goldCount.text = gold.ToString();
	}

	public void UpdateAvailableTowers(int gold, List<TowerData> availableTowers)
	{
		//first remove all buttons
		foreach (Transform child in _towerContainer)
		{
			Destroy(child.gameObject);
		}

		var rectTransform = TowerButtonPrefab.GetComponent<RectTransform>();
		foreach (var towerData in availableTowers)
		{
			GameObject button = (GameObject)Instantiate(TowerButtonPrefab);
			button.transform.SetParent(_towerContainer);

			//disable if not engough gold
			if (gold < towerData.Cost)
			{
				button.GetComponent<Button>().interactable = false;
			}

			//assign the towerdata to the button
			button.GetComponent<TowerButton>().SetData(towerData);
		}
	}
}
