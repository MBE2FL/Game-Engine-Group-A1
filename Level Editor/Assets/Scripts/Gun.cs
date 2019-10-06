using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Transform _bulletSpawn;
    private Object _bulletPrefab;
    [SerializeField]
    private float _reloadTime = 2.0f;
    private float _currReloadTime = 0.0f;
    [SerializeField]
    private int _clipSize = 25;
    [SerializeField]
    private int _currentClip = 0;

    // Start is called before the first frame update
    void Start()
    {
        _bulletSpawn = transform.Find("BulletSpawn");
        _bulletPrefab = Resources.Load("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        // Reload
        if (_currentClip >= _clipSize)
        {
            _currReloadTime += Time.deltaTime;

            if (_currReloadTime >= _reloadTime)
                _currentClip = 0;
        }

        // Fire the gun
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj;
            obj = Instantiate(_bulletPrefab, _bulletSpawn.position, Quaternion.identity) as GameObject;
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 20.0f);
            ++_currentClip;
        }
    }
}
