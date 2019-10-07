using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyCommand : ICommand
{
    GameObject obj = null;

    public void Execute()
    {
        Factory.Instance.CreateGameObject(ObjectTypes.Bunny, out obj);
    }

    public void Undo()
    {
        Factory.Instance.DeleteGameObject(ref obj);
    }
}
