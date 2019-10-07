using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public override GameObject clone()
    {
        GameObject obj;
        Factory.Instance.CreateGameObject(ObjectTypes.Zombie, out obj);
        return obj;
    }
}
