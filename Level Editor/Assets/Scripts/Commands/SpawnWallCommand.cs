using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWallCommand : ICommand
{
    GameObject obj = null;

    public void Execute()
    {
        Factory.Instance.CreateGameObject(ObjectTypes.Wall, out obj);
    }

    public void Undo()
    {
        Factory.Instance.DeleteGameObject(ref obj);
    }
}
