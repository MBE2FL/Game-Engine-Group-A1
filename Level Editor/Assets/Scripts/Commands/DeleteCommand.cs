using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCommand : ICommand
{
    GameObject _obj = null;
    ObjectTypes _type = ObjectTypes.Cube;


    public void Execute()
    {
        _obj = Camera.main.GetComponent<CameraControl>().SelectedObj;
        _type = _obj.GetComponent<ObjectType>().Type;
        Factory.Instance.DeleteGameObject(ref _obj);
    }

    public void Undo()
    {
        Factory.Instance.CreateGameObject(_type, out _obj);
    }

    public GameObject getObj()
    {
        return _obj;
    }
}
