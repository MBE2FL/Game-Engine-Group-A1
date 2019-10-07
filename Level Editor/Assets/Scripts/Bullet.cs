using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int _damage = 50;
    //[SerializeField]
    //private int _force = 10;
    private bool _active = false;

    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_active)
        {
            GameObject obj = collision.gameObject;


            if (obj.tag == "Enemy")
            {
                ContactPoint contactPoint = collision.GetContact(0);

                //Vector3 force = -contactPoint.normal * _force;
                //collision.rigidbody.AddForceAtPosition(force, contactPoint.point);

                collision.gameObject.GetComponent<Health>().HP -= _damage;

                BulletPool.Instance.reclaim(gameObject);
            }
        }
    }
}
