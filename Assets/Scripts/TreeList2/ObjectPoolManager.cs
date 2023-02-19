using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();

    public static ObjectPoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public T GetObject<T>(string poolName) where T : MonoBehaviour
    {
        if (!pools.ContainsKey(poolName))
        {
            pools[poolName] = new ObjectPool(Resources.Load<T>(poolName));
        }
        return pools[poolName].GetObject<T>();
    }

    public void ReturnObject(string poolName, MonoBehaviour obj)
    {
        if (pools.ContainsKey(poolName))
        {
            pools[poolName].ReturnObject(obj);
        }
    }
}
