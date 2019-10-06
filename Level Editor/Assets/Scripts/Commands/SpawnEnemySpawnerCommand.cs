using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemySpawnerCommand : ICommand
{
    GameObject obj = null;

    public void Execute()
    {
        Factory.Instance.CreateGameObject(ObjectTypes.EnemySpawner, out obj);
    }

    public void Undo()
    {
        Factory.Instance.DeleteGameObject(ref obj);
    }
}
