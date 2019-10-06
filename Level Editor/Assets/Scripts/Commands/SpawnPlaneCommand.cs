using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlaneCommand : ICommand
{
    GameObject obj = null;

    public void Execute()
    {
        Factory.Instance.CreateGameObject(ObjectTypes.Plane, out obj);
    }

    public void Undo()
    {
        Factory.Instance.DeleteGameObject(ref obj);
    }
}
