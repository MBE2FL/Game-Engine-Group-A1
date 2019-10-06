using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphereCommand : ICommand
{
    GameObject _obj = null;

    public void Execute()
    {
        Factory.Instance.CreateGameObject(ObjectTypes.Sphere, out _obj);
        //Factory.Instance.CreateGameObject("Sphere", out obj);
    }

    public void Undo()
    {
        //Destroy(obj);
        Factory.Instance.DeleteGameObject(ref _obj);
    }

    public GameObject getObj()
    {
        return _obj;
    }
}
