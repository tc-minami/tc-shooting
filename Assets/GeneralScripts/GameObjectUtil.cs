using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtil
{

    public static T CreateInstance<T>(Transform _parent, string _name) where T : MonoBehaviour
    {
        GameObject instance = new GameObject(_name);
        if (_parent != null) instance.transform.SetParent(_parent);
        instance.AddComponent<T>();
        return instance.GetComponent<T>();
    }

    public static GameObject CreateInstance(Transform _parent, string _name)
    {
        GameObject instance = new GameObject(_name);
        if (_parent != null) instance.transform.SetParent(_parent);
        return instance;
    }
}
