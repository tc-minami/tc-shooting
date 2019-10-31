using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public GenericPool Pool { private get; set; }
    [SerializeField]
    private bool isDestroyOnReturnPool = false;

    public void ReserveDestroyOnReturnPool(bool _doDestroy = true)
    {
        isDestroyOnReturnPool = _doDestroy;
    }

    protected void Return2Pool()
    {
        if (isDestroyOnReturnPool)
        {
            Destroy(gameObject);
        }
        else
        {
            Pool.ReturnPooledObject(this);
        }
    }

    //protected new void Destroy(Object _obj)
    //{
    //    Return2Pool();
    //}
}
