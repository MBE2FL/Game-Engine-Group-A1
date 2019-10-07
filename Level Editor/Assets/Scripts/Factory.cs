using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Factory : MonoBehaviour
{

    private static Factory _instance = null;

    private Object _cube;
    private Object _sphere;
    private Object _player;
    private Object _plane;
    private Object _bunny;
    private Object _wall;
    private Object _enemySpawner;
    private Object _zombie;

    private List<GameObject> _objs = new List<GameObject>();


    public static Factory Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject obj = new GameObject("Factory");
                _instance = obj.AddComponent<Factory>();
            }

            return _instance;
        }
    }

    public List<GameObject> Objs
    {
        get
        {
            return _objs;
        }
    }

    private void Awake()
    {
        if (!_instance && (!_instance == this))
            _instance = this;

        _cube = Resources.Load("Prefabs/Cube");
        _sphere = Resources.Load("Prefabs/Sphere");
        _player = Resources.Load("Prefabs/Player");
        _plane = Resources.Load("Prefabs/Plane");
        _bunny = Resources.Load("Prefabs/Bunny");
        _wall = Resources.Load("Prefabs/Wall");
        _enemySpawner = Resources.Load("Prefabs/EnemySpawner");
        _zombie = Resources.Load("Prefabs/Zombie");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateGameObject(ObjectTypes type, out GameObject obj)
    {
        //if (prefabName == "Cube")
        //{
        //    obj = Instantiate(cube, Vector3.zero, Quaternion.identity, null);
        //    obj.GetComponent<ObjectType>().Type = ObjectTypes.Cube;
        //}
        //else if (prefabName == "Sphere")
        //{
        //    obj = Instantiate(sphere, Vector3.zero, Quaternion.identity, null);
        //    obj.GetComponent<ObjectType>().Type = ObjectTypes.Sphere;
        //}

        switch (type)
        {
            case ObjectTypes.Cube:
                obj = Instantiate(_cube, Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.GetComponent<ObjectType>().Type = ObjectTypes.Cube;
                _objs.Add(obj);
                break;
            case ObjectTypes.Sphere:
                obj = Instantiate(_sphere, Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.GetComponent<ObjectType>().Type = ObjectTypes.Sphere;
                _objs.Add(obj);
                break;
            case ObjectTypes.Player:
                obj = Instantiate(_player, Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.GetComponent<ObjectType>().Type = ObjectTypes.Player;
                break;
            case ObjectTypes.Bunny:
                obj = Instantiate(_bunny, Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.GetComponent<ObjectType>().Type = ObjectTypes.Bunny;
                break;
            case ObjectTypes.Wall:
                obj = Instantiate(_wall, Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.GetComponent<ObjectType>().Type = ObjectTypes.Wall;
                _objs.Add(obj);
                break;
            case ObjectTypes.EnemySpawner:
                obj = Instantiate(_enemySpawner, Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.GetComponent<ObjectType>().Type = ObjectTypes.EnemySpawner;
                _objs.Add(obj);
                break;
            case ObjectTypes.Zombie:
                obj = Instantiate(_zombie, Vector3.zero, Quaternion.identity, null) as GameObject;
                obj.GetComponent<ObjectType>().Type = ObjectTypes.Zombie;
                break;
            default:
                obj = null;
                break;
        }

        //_objs.Add(obj);
    }

    public void DeleteGameObject(ref GameObject obj)
    {
        if (_objs.Contains(obj))
        {
            if (_objs.Count > 1)
            {
                GameObject temp = _objs[_objs.Count - 1];
                _objs[_objs.IndexOf(obj)] = temp;
            }
            _objs.RemoveAt(_objs.Count - 1);

        }

        Destroy(obj);
    }

    public ICommand CreateCommand(CommandTypes commandType)
    {
        ICommand command = null;

        switch (commandType)
        {
            case CommandTypes.SpawnCube:
                command = new SpawnCubeCommand();
                break;
            case CommandTypes.SpawnSphere:
                command = new SpawnSphereCommand();
                break;
            case CommandTypes.SpawnPlayer:
                command = new SpawnPlayerCommand();
                break;
            case CommandTypes.Delete:
                command = new DeleteCommand();
                break;
            case CommandTypes.SpawnWall:
                command = new SpawnWallCommand();
                break;
            case CommandTypes.SpawnEnemySpawner:
                command = new SpawnEnemySpawnerCommand();
                break;
        }

        return command;
    }
}
