using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    GameObject _player;
    Transform _playerTrans;
    NavMeshAgent _navAgent;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player)
            _playerTrans = _player.transform;
        _navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player)
            _navAgent.SetDestination(_playerTrans.position);
        else
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            if (_player)
                _playerTrans = _player.transform;
        }
    }
}
