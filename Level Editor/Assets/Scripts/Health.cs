using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _health = 100;

    public int GetHealth
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
        }
    }
}
