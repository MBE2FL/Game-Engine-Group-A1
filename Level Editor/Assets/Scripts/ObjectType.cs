using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    private ObjectTypes _type;

    public ObjectTypes Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }
}
