using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Enemy
{
    public override GameObject clone()
    {
        GameObject obj;
        Factory.Instance.CreateGameObject(ObjectTypes.Bunny, out obj);
        return obj;
    }
}
