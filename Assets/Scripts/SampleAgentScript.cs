using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Mirror;

public class SampleAgentScript : NetworkBehaviour
{

    private Transform[] _targets;
    private int _tgconter = 0;
    NavMeshAgent _agent;
    public GameObject[] _wPoints;
    //private CreepProperties crpPro;
    private NavMeshAgent _navAgent;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> wPointsList = _wPoints.OrderBy(name => name.name).ToList();

        _targets = new Transform[_wPoints.Length];
        for (int i = 0; i<_wPoints.Length; i++)
        {
            _targets[i] = wPointsList[i].transform;
        }

        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
        NextTarget();
    }

    void NextTarget()
    {
        if (isServer)
        {
            if (_targets.Length == 0)
                return;
            _agent.destination = _targets[_tgconter].position;
            _tgconter = (_tgconter + 1) % _targets.Length;
        }        
    }

    // Update is called once per frame
    void Update()
    {
		// if (isServer)
        // {
		// 	transform.position = Vector3.MoveTowards(transform.position, _targets[0].position, 5 * Time.deltaTime);
		// }
        //_agent.SetDestination(_targets[0].position);
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            NextTarget();
    }
}
