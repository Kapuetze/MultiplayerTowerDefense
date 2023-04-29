using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onCreepSpawnTrigger;
    public event Action onCreepDiedTrigger;

    public void OnCreepSpawn()
    {
        if(onCreepSpawnTrigger != null)
        {
            onCreepSpawnTrigger();
        }
    }

    public void OnCreepDied()
    {
        if (onCreepDiedTrigger != null)
        {
            onCreepDiedTrigger();
        }
    }

}
