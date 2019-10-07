using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //[SerializeField]
    //private float _reloadTime = 2.0f;
    //private float _currReloadTime = 0.0f;
    //[SerializeField]
    //private int _clipSize = 25;
    //[SerializeField]
    //private int _currentClip = 0;
    private BulletPool _bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _bulletPool = BulletPool.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Reload
        //if (_currentClip >= _clipSize)
        //{
        //    _currReloadTime += Time.deltaTime;

        //    if (_currReloadTime >= _reloadTime)
        //        _currentClip = 0;
        //}

        // Fire the gun
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj;
            //obj = Instantiate(_bulletPrefab, _bulletSpawn.position, Quaternion.identity) as GameObject;
            obj = _bulletPool.getBullet();
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * 3000.0f);
            //++_currentClip;
        }
    }
}
