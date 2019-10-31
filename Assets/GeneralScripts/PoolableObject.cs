using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public GenericPool Pool { private get; set; }

    protected void Return2Pool()
    {
        //Pool.Return(this);
    }

    protected new void Destroy(Object _obj)
    {
        Return2Pool();
    }
}
