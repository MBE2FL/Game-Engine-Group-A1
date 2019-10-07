using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    private List<GameObject> _inactivePool;
    private List<GameObject> _activePool;
    [SerializeField]
    private int _size = 10;
    private Transform _bulletSpawn;
    private static BulletPool _instance;

    public static BulletPool Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject obj = new GameObject("BulletPool");
                _instance = obj.AddComponent<BulletPool>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (!_instance && (!_instance == this))
            _instance = this;

        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        _bulletSpawn = transform.Find("BulletSpawn");

        // Initiate bullet pools
        _inactivePool = new List<GameObject>();
        _activePool = new List<GameObject>();
        GameObject obj;

        for (int i = 0; i < _size; ++i)
        {
            obj = Instantiate(_bullet, _bulletSpawn.transform);
            _inactivePool.Add(obj);

            obj.hideFlags = HideFlags.HideInHierarchy;
            obj.SetActive(false);
        }
    }


    public GameObject getBullet()
    {
        GameObject bullet;

        // Use from the inactive pool
        if (_inactivePool.Count > 0)
        {
            bullet = _inactivePool[_inactivePool.Count - 1];
            _inactivePool.RemoveAt(_inactivePool.Count - 1);
            _activePool.Add(bullet);
        }
        // Use from the active pool
        else
        {
            bullet = _activePool[0];
            GameObject temp = _activePool[_activePool.Count - 1];
            _activePool[0] = temp;
            _activePool[_activePool.Count - 1] = bullet;
        }

        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Active = true;

        // Reset the current bullet
        bullet.transform.position = _bulletSpawn.transform.position;
        bullet.transform.rotation = _bulletSpawn.transform.rotation;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;

        return bullet;
    }

    public void reclaim(GameObject bullet)
    {
        // Reclaim the current bullet.
        _inactivePool.Add(bullet);
        
        if (_activePool.Count > 1)
        {
            GameObject temp = _activePool[_activePool.Count - 1];
            _activePool[_activePool.IndexOf(bullet)] = temp;
        }
        _activePool.RemoveAt(_activePool.Count - 1);

        bullet.SetActive(false);
        bullet.GetComponent<Bullet>().Active = false;
    }
}
