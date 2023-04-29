using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Levels;
using Players;

public class UnserNetworkManager : NetworkManager
{
    private LevelManager _levelManager;
	private int _numConnections = 0;
	private Color[] _playerColors = new Color[2] 
	{
		Color.red,
		Color.blue
	};
    public override void OnServerConnect(NetworkConnection conn)
    {
		_numConnections++;
		_levelManager.StartSpawner();

		Debug.Log("Connections: " + _numConnections);

		// playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject gameobject = Instantiate(playerPrefab);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        Builder builder = gameobject.GetComponent<Builder>();
        Player player = gameobject.GetComponent<Player>();
        builder.SetPlayerColor(_playerColors[numPlayers]);

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);

        base.OnServerConnect(conn);
    }

    public override void OnStartServer()
    {
        _levelManager = GetComponent<LevelManager>();
        base.OnStartServer();
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        _numConnections = 0;
        _levelManager.StopSpawner();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Tower");
        foreach(GameObject go in gos)
        {
            Destroy(go);
        }
    }
}
