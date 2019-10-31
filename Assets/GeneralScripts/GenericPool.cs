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

    public static GenericPool CreateNewPoolWithPrefab(PoolableObject poolableObjectPrefab, Transform _parent, string _poolName = "Pool", string _poolObjName = "PoolObj", int _maxPoolSize = NoSizeLimit)
    {
        GenericPool pool = GameObjectUtil.CreateInstance<GenericPool>(_parent, _poolName);
        pool.maxPoolSize = _maxPoolSize;
        pool.InitializePool();

        pool.pooledObject = poolableObjectPrefab;
        return pool;
    }

    public static GenericPool CreateNewPool<T>(Transform _parent, string _poolName = "Pool", string _poolObjName = "PoolObj", int _maxPoolSize = NoSizeLimit) where T : PoolableObject
    {
        GenericPool pool = GameObjectUtil.CreateInstance<GenericPool>(_parent, _poolName);
        pool.maxPoolSize = _maxPoolSize;
        pool.InitializePool();

        T poolObj = GameObjectUtil.CreateInstance<T>(pool.transform, _poolObjName);
        pool.pooledObject = poolObj;

        return pool;
    }

    public T GetOrCreate<T>() where T : PoolableObject
    {
        return GetOrCreate() as T;
    }

    public PoolableObject GetOrCreate()
    {
        // Just to make sure pool is initialized.
        InitializePool();

        PoolableObject poolableObject;

        if (pool.Count > 0)
        {
            poolableObject = pool.Dequeue();
            poolableObject.gameObject.SetActive(true);
        }
        else
        {
            if (pooledObject == null)
            {
                Debug.LogError("Pooled Object is not set.");
                return null;
            }
            poolableObject = Instantiate(pooledObject);
            poolableObject.Pool = this;
        }
        poolableObject.transform.SetParent(this.transform);
        return poolableObject;
    }

    public void Return(PoolableObject _poolableObject)
    {
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
