using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForwartsMovementBehaviour : MonoBehaviour
{
    private NavMeshAgent _agent;
    private int _currentWayPoint;
    private Transform[] _waypoints;

    public Vector3 target;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _waypoints = FindFirstObjectByType<WayPointBehaviour>().GetWayPoints();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _currentWayPoint = 0;
        _agent.SetDestination(_waypoints[_currentWayPoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(_agent.transform.position, _agent.destination) < 0.1f)
            GetNewDestination();   
    }
    
    void GetNewDestination(){
        if(_currentWayPoint < _waypoints.Length -1){

            _currentWayPoint++;
            _agent.SetDestination(_waypoints[_currentWayPoint].position);
        }
    }

}
