using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool : MonoBehaviour
{
    [SerializeField]
    private PoolableObject pooledObject;

    private const int NoSizeLimit = -1;
    private int maxPoolSize = NoSizeLimit;
    private Queue<PoolableObject> pool;

    public static GenericPool CreateNewPoolWithPrefab(PoolableObject _poolableObjectPrefab, Transform _parent, string _poolName = "Pool", string _poolObjName = "PoolObj", int _maxPoolSize = NoSizeLimit)
    {
        GenericPool pool = CreateNewPool(_parent, _poolName, _maxPoolSize);
        pool.SetPoolableObject(_poolableObjectPrefab, _poolObjName);
        return pool;
    }

    public static GenericPool CreateNewPool<T>(Transform _parent, string _poolName = "Pool", string _poolObjName = "PoolObj", int _maxPoolSize = NoSizeLimit) where T : PoolableObject
    {
        GenericPool pool = CreateNewPool(_parent, _poolName, _maxPoolSize);
        T poolObj = GameObjectUtil.CreateInstance<T>(pool.transform, _poolObjName);
        pool.SetPoolableObject(poolObj, _poolObjName);
        return pool;
    }

    public static GenericPool CreateNewPool(Transform _parent, string _poolName = "Pool", int _maxPoolSize = NoSizeLimit)
    {
        GenericPool pool = GameObjectUtil.CreateInstance<GenericPool>(_parent, _poolName);
        pool.maxPoolSize = _maxPoolSize;
        pool.InitializePool();
        return pool;
    }

    public void SetPoolableObject(PoolableObject _poolableObject, string _poolObjName, bool _removeCurrentPoolObj = true)
    {
        pooledObject = _poolableObject;
        pooledObject.name = _poolObjName;

        if (_removeCurrentPoolObj)
        {
            ClearPooledObjects(false);
        }
    }

    public T GetOrCreate<T>() where T : PoolableObject
    {
        return GetOrCreate() as T;
    }

    public PoolableObject GetOrCreate()
    {
        // Just to make sure pool is initialized.
        InitializePool();

        PoolableObject obj = GetPooledObject();

        if (obj == null)
        {
            if (pooledObject == null)
            {
                Debug.LogError("PoolableObject prefab is not set.");
                return null;
            }
            obj = Instantiate(pooledObject);
            obj.Pool = this;
        }

        obj.transform.SetParent(this.transform);
        return obj;
    }

    public void ClearPooledObjects(bool _clearRightNow = false)
    {
        foreach (Transform t in this.transform)
        {
            if (_clearRightNow)
            {
                Destroy(t.gameObject);
            }
            else
            {
                PoolableObject poolObj = t.GetComponent<PoolableObject>();
                if (poolObj != null && !pool.Contains(poolObj))
                {
                    poolObj.ReserveDestroyOnReturnPool(true);
                }
                else
                {
                    Destroy(t.gameObject);
                }

            }
        }

        PoolableObject obj = GetPooledObject();
        while (obj != null)
        {
            Destroy(obj);
            obj = GetPooledObject();
        }
    }

    private PoolableObject GetPooledObject()
    {
        PoolableObject poolableObject = null;
        while (pool.Count > 0 && poolableObject == null)
        {
            poolableObject = pool.Dequeue();
        }
        if (poolableObject != null) poolableObject.gameObject.SetActive(true);
        return poolableObject;
    }

    public void ReturnPooledObject(PoolableObject _poolableObject)
    {
        if (_poolableObject == null)
        {
            return;
        }
        _poolableObject.gameObject.SetActive(false);
        pool.Enqueue(_poolableObject);
    }

    private void InitializePool()
    {
        if (pool != null) return;
        if (maxPoolSize == NoSizeLimit)
        {
            pool = new Queue<PoolableObject>();
        }
        else
        {
            pool = new Queue<PoolableObject>(maxPoolSize);
        }
    }
}
