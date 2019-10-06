using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubeCommand : ICommand
{
    GameObject obj = null;

    public void Execute()
    {
        Factory.Instance.CreateGameObject(ObjectTypes.Cube, out obj);
        //Factory.Instance.CreateGameObject("Cube", out obj);
    }

    public void Undo()
    {
        //Destroy(obj);
        Factory.Instance.DeleteGameObject(ref obj);
    }
}
