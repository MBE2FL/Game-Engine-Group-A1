using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int _maxEnemies = 10;
    private int _numEnemies = 0;
    [SerializeField]
    private int _spawnRate = 1;
    private Factory _factory = null;
    [SerializeField]
    private float _spawnTime = 0.5f;
    private float _currSpawnTime = 0.0f;
    [SerializeField]
    private bool _active = false;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        _factory = Factory.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_active && (enemy != null))
        {
            if ((_currSpawnTime >= _spawnTime) && (_numEnemies < _maxEnemies))
            {
                // Spawn enemy from factory
                GameObject obj;
                for (int i = 0; i < _spawnRate; ++i)
                {
                    //_factory.CreateGameObject(ObjectTypes.Enemy, out obj);
                    obj = enemy.clone();
                    obj.transform.position = transform.position;
                    ++_numEnemies;

                    if (_numEnemies >= _maxEnemies)
                        break;
                }

                _currSpawnTime = 0.0f;
            }

            _currSpawnTime += Time.deltaTime;
        }
    }
}
