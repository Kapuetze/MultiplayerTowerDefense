using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.EventSystem;

public class TowerButton : MonoBehaviour
{
	public TowerData Data;

	private Text _name;
    // Start is called before the first frame update
    void Awake()
    {
        _name = transform.Find("Name").GetComponent<Text>();
    }

    public void SetData(TowerData data)
	{
		Data = data;
		_name.text = Data.Name;
	}

	public void SelectTowerToBuild()
	{
		var ev = new BuildTowerSelectedEvent 
		{
			TowerData = Data
		};

		ev.FireEvent();
	}
}
