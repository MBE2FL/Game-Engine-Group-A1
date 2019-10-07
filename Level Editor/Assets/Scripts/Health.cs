using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _hp = 100;

    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }

    private void Update()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);

            ++EnemySpawner.Kills;
            Debug.Log("Kills: " + EnemySpawner.Kills);
        }
    }
}
